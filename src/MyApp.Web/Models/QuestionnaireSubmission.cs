using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models;

public sealed class QuestionnaireSubmission
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string QuestionnaireType { get; set; } = QuestionnaireCatalog.StudentVisaType;

    [Required]
    public string AnswersProtected { get; set; } = string.Empty;

    public int QuestionnaireVersion { get; set; } = FormQuestionnaire.CurrentVersion;

    public DateTime CreatedAtUtc { get; set; }

    public bool IsDraft { get; set; }
}
