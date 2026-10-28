namespace MyApp.Web.Models;

public sealed class FormSubmissionInputModel
{
    public Dictionary<string, string> Answers { get; set; } = new(StringComparer.Ordinal);

    public string QuestionnaireType { get; set; } = QuestionnaireCatalog.StudentVisaType;

    public string QuestionnaireTitle { get; set; } = QuestionnaireCatalog.StudentVisa.Title;

    public IReadOnlyList<string> TabTitles { get; set; } = QuestionnaireCatalog.StudentVisa.TabTitles;

    public IReadOnlyList<FormQuestionnaireSection> Sections { get; set; } = FormQuestionnaire.Sections;

    public int? DraftId { get; set; }

    public int ActiveSection { get; set; } = 1;
}
