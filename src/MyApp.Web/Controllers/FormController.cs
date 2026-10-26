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
    private static readonly Dictionary<string, (FormQuestionnaireSection Section, FormQuestion Question)> QuestionsByKey =
        FormQuestionnaire.Sections
            .SelectMany(section => section.Questions.Select(question => (section, question)))
            .ToDictionary(item => item.question.Key, item => (item.section, item.question), StringComparer.Ordinal);

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
    public async Task<IActionResult> Details(int id)
    {
        var userId = userManager.GetUserId(User);
        var submission = await database.QuestionnaireSubmissions
            .SingleOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if (submission is null)
        {
            return NotFound();
        }

        var answers = JsonSerializer.Deserialize<Dictionary<string, string>>(
            answersProtector.Unprotect(submission.AnswersProtected))
            ?? throw new InvalidDataException("The saved questionnaire answers are invalid.");

        var sections = FormQuestionnaire.Sections
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
                            answers[question.Key + suffix]))
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
            submission.QuestionnaireVersion,
            submission.CreatedAtUtc,
            sections));
    }

    [HttpGet]
    public IActionResult Create() => View(new FormSubmissionInputModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FormSubmissionInputModel model)
    {
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
                !QuestionsByKey.TryGetValue(match.Groups["key"].Value, out var definition) ||
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

        if (!cleanedAnswers.TryGetValue("declarationConfirmed", out var declaration) ||
            !string.Equals(declaration, "Yes", StringComparison.Ordinal))
        {
            ModelState.AddModelError(
                "Answers[declarationConfirmed]",
                "Confirm the declaration before submitting the questionnaire.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = userManager.GetUserId(User);
        if (userId is null)
        {
            return Challenge();
        }

        database.QuestionnaireSubmissions.Add(new QuestionnaireSubmission
        {
            UserId = userId,
            AnswersProtected = answersProtector.Protect(JsonSerializer.Serialize(cleanedAnswers)),
            QuestionnaireVersion = FormQuestionnaire.CurrentVersion,
            CreatedAtUtc = DateTime.UtcNow
        });
        await database.SaveChangesAsync();

        TempData["SuccessMessage"] = "Your student visa questionnaire was submitted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private static bool IsValidAnswer(FormQuestion question, string value)
    {
        if (value.Length == 0)
        {
            return true;
        }

        return question.Type switch
        {
            FormQuestionType.YesNo =>
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
