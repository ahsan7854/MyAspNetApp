namespace MyApp.Web.Models;

public sealed record QuestionnaireAnswerViewModel(
    string Label,
    string Value,
    string? GroupTitle = null);

public sealed record QuestionnaireSectionViewModel(
    int Number,
    string Title,
    int RecordNumber,
    IReadOnlyList<QuestionnaireAnswerViewModel> Answers);

public sealed record QuestionnaireSubmissionDetailsViewModel(
    int Id,
    string QuestionnaireTitle,
    int QuestionnaireVersion,
    DateTime CreatedAtUtc,
    IReadOnlyList<QuestionnaireSectionViewModel> Sections);
