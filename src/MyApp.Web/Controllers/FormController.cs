using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data;
using MyApp.Web.Models;

namespace MyApp.Web.Controllers;

[Authorize]
public sealed class FormController(
    ApplicationDbContext database,
    UserManager<ApplicationUser> userManager,
    IDataProtectionProvider dataProtectionProvider) : Controller
{
    private readonly IDataProtector answersProtector =
        dataProtectionProvider.CreateProtector("MyApp.QuestionnaireAnswers.v1");
    private const int MaximumAnswers = 500;
    private const int MaximumAnswerLength = 4000;
    private const int MaximumTotalAnswerLength = 100_000;
    private static readonly Regex AnswerKeyPattern = new(
        "^(?<key>[A-Za-z][A-Za-z0-9]*)(?:__(?<repeat>[2-9]|1[0-9]|20))?$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [HttpGet]
    public IActionResult Particulars() => View();

    public async Task<IActionResult> Index()
    {
        var userId = userManager.GetUserId(User);
        var submissions = await database.QuestionnaireSubmissions
            .Where(submission => submission.UserId == userId)
            .OrderByDescending(submission => submission.CreatedAtUtc)
            .ToListAsync();
        return View(submissions);
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        int? draftId,
        int section = 1,
        bool newDraft = false,
        string? questionnaireType = null)
    {
        var userId = userManager.GetUserId(User);
        var selectedType = questionnaireType ?? QuestionnaireCatalog.StudentVisaType;
        if (!QuestionnaireCatalog.TryGet(selectedType, out var questionnaire))
        {
            return BadRequest("The selected questionnaire is not available.");
        }

        QuestionnaireSubmission? draft = null;
        if (draftId.HasValue)
        {
            draft = await database.QuestionnaireSubmissions
                .SingleOrDefaultAsync(item =>
                    item.Id == draftId.Value && item.UserId == userId && item.IsDraft);
            if (draft is null)
            {
                return NotFound();
            }

            if (!QuestionnaireCatalog.TryGet(draft.QuestionnaireType, out questionnaire))
            {
                throw new InvalidDataException("The saved questionnaire type is not supported.");
            }
        }
        else if (!newDraft)
        {
            draft = await database.QuestionnaireSubmissions
                .Where(item => item.UserId == userId &&
                    item.IsDraft &&
                    item.QuestionnaireType == questionnaire.Type)
                .OrderByDescending(item => item.CreatedAtUtc)
                .FirstOrDefaultAsync();
        }

        var answers = draft is null
            ? new Dictionary<string, string>(StringComparer.Ordinal)
            : JsonSerializer.Deserialize<Dictionary<string, string>>(
                answersProtector.Unprotect(draft.AnswersProtected))
              ?? throw new InvalidDataException("The saved questionnaire answers are invalid.");

        return View("Create", CreateInputModel(
            questionnaire, draft?.Id, section, answers));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var userId = userManager.GetUserId(User);
        var submission = await database.QuestionnaireSubmissions
            .SingleOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if (submission is null)
        {
            return NotFound();
        }

        if (!QuestionnaireCatalog.TryGet(submission.QuestionnaireType, out var questionnaire))
        {
            throw new InvalidDataException("The saved questionnaire type is not supported.");
        }

        var answers = JsonSerializer.Deserialize<Dictionary<string, string>>(
            answersProtector.Unprotect(submission.AnswersProtected))
            ?? throw new InvalidDataException("The saved questionnaire answers are invalid.");

        var sections = questionnaire.Sections
            .SelectMany(section =>
            {
                var recordNumbers = section.Questions
                    .SelectMany(question => answers.Keys
                        .Where(key => key == question.Key ||
                            key.StartsWith(question.Key + "__", StringComparison.Ordinal))
                        .Select(GetRecordNumber))
                    .Distinct()
                    .Order()
                    .ToArray();

                return recordNumbers.Select(recordNumber =>
                {
                    var suffix = recordNumber == 1 ? string.Empty : $"__{recordNumber}";
                    var sectionAnswers = section.Questions
                        .Where(question => answers.ContainsKey(question.Key + suffix))
                        .Select(question => new QuestionnaireAnswerViewModel(
                            question.Label,
                            answers[question.Key + suffix],
                            question.GroupTitle))
                        .ToArray();
                    return new QuestionnaireSectionViewModel(
                        section.Number,
                        section.Title,
                        recordNumber,
                        sectionAnswers);
                });
            })
            .ToArray();

        return View(new QuestionnaireSubmissionDetailsViewModel(
            submission.Id,
            questionnaire.Title,
            submission.QuestionnaireVersion,
            submission.CreatedAtUtc,
            sections));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        FormSubmissionInputModel model,
        string intent = "save")
    {
        var userId = userManager.GetUserId(User);
        if (userId is null)
        {
            return Challenge();
        }

        QuestionnaireSubmission? submission = null;
        QuestionnaireDefinition questionnaire;
        if (model.DraftId.HasValue)
        {
            submission = await database.QuestionnaireSubmissions
                .SingleOrDefaultAsync(item =>
                    item.Id == model.DraftId.Value && item.UserId == userId && item.IsDraft);
            if (submission is null)
            {
                return NotFound();
            }

            if (!QuestionnaireCatalog.TryGet(submission.QuestionnaireType, out questionnaire))
            {
                throw new InvalidDataException("The saved questionnaire type is not supported.");
            }
        }
        else if (!QuestionnaireCatalog.TryGet(model.QuestionnaireType, out questionnaire))
        {
            return BadRequest("The selected questionnaire is not available.");
        }

        model.QuestionnaireType = questionnaire.Type;
        model.QuestionnaireTitle = questionnaire.Title;
        model.TabTitles = questionnaire.TabTitles;
        model.Sections = questionnaire.Sections;
        model.ActiveSection = Math.Clamp(
            model.ActiveSection, 1, questionnaire.Sections.Count);
        var questionsByKey = questionnaire.Sections
            .SelectMany(section => section.Questions.Select(question => (section, question)))
            .ToDictionary(
                item => item.question.Key,
                item => (Section: item.section, Question: item.question),
                StringComparer.Ordinal);
        var cleanedAnswers = new Dictionary<string, string>(StringComparer.Ordinal);
        var totalAnswerLength = 0;

        if (model.Answers.Count > MaximumAnswers)
        {
            ModelState.AddModelError(string.Empty, "The form contains too many answers.");
        }

        foreach (var (postedKey, postedValue) in model.Answers.Take(MaximumAnswers))
        {
            var match = AnswerKeyPattern.Match(postedKey);
            if (!match.Success ||
                !questionsByKey.TryGetValue(match.Groups["key"].Value, out var definition) ||
                (match.Groups["repeat"].Success && !definition.Section.Repeatable))
            {
                ModelState.AddModelError(string.Empty, "The form contains an unrecognized field.");
                continue;
            }

            var value = postedValue?.Trim() ?? string.Empty;
            totalAnswerLength += value.Length;
            if (value.Length > MaximumAnswerLength)
            {
                ModelState.AddModelError(
                    $"Answers[{postedKey}]",
                    $"The answer cannot exceed {MaximumAnswerLength} characters.");
                continue;
            }

            if (!IsValidAnswer(definition.Question, value))
            {
                ModelState.AddModelError(
                    $"Answers[{postedKey}]",
                    "Enter a valid response for this field.");
                continue;
            }

            if (value.Length > 0)
            {
                cleanedAnswers[postedKey] = value;
            }
        }

        if (totalAnswerLength > MaximumTotalAnswerLength)
        {
            ModelState.AddModelError(string.Empty, "The combined form answers are too long.");
        }

        var isSubmitting = string.Equals(intent, "submit", StringComparison.Ordinal);
        if (isSubmitting &&
            (!cleanedAnswers.TryGetValue("declarationConfirmed", out var declaration) ||
             !string.Equals(declaration, "Yes", StringComparison.Ordinal)))
        {
            ModelState.AddModelError(
                "Answers[declarationConfirmed]",
                "Confirm the declaration before submitting the questionnaire.");
        }

        if (!ModelState.IsValid)
        {
            return View("Create", model);
        }

        submission ??= new QuestionnaireSubmission
        {
            UserId = userId,
            CreatedAtUtc = DateTime.UtcNow
        };
        submission.QuestionnaireType = questionnaire.Type;
        submission.AnswersProtected = answersProtector.Protect(
            JsonSerializer.Serialize(cleanedAnswers));
        submission.QuestionnaireVersion = questionnaire.Version;
        submission.IsDraft = !isSubmitting;
        if (database.Entry(submission).State == EntityState.Detached)
        {
            database.QuestionnaireSubmissions.Add(submission);
        }

        await database.SaveChangesAsync();

        if (!isSubmitting)
        {
            TempData["SuccessMessage"] = "Your progress has been saved.";
            var nextSection = string.Equals(intent, "continue", StringComparison.Ordinal)
                ? Math.Min(model.ActiveSection + 1, questionnaire.Sections.Count)
                : model.ActiveSection;
            return RedirectToAction(nameof(Create), new
            {
                draftId = submission.Id,
                section = nextSection,
                questionnaireType = questionnaire.Type
            });
        }

        TempData["SuccessMessage"] = $"Your {questionnaire.Title} was submitted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private static FormSubmissionInputModel CreateInputModel(
        QuestionnaireDefinition questionnaire,
        int? draftId,
        int section,
        Dictionary<string, string> answers) =>
        new()
        {
            DraftId = draftId,
            QuestionnaireType = questionnaire.Type,
            QuestionnaireTitle = questionnaire.Title,
            TabTitles = questionnaire.TabTitles,
            Sections = questionnaire.Sections,
            ActiveSection = Math.Clamp(section, 1, questionnaire.Sections.Count),
            Answers = answers
        };

    private static bool IsValidAnswer(FormQuestion question, string value)
    {
        if (value.Length == 0)
        {
            return true;
        }

        return question.Type switch
        {
            FormQuestionType.YesNo or FormQuestionType.Checkbox =>
                value is "Yes" or "No",
            FormQuestionType.Email =>
                System.Net.Mail.MailAddress.TryCreate(value, out _),
            FormQuestionType.Date =>
                DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
            FormQuestionType.Number =>
                int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out _),
            _ => true
        };
    }

    private static int GetRecordNumber(string key)
    {
        var separator = key.LastIndexOf("__", StringComparison.Ordinal);
        return separator < 0
            ? 1
            : int.Parse(key[(separator + 2)..], CultureInfo.InvariantCulture);
    }
}
