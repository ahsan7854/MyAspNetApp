namespace MyApp.Web.Models;

public sealed class FormSubmissionInputModel
{
    public Dictionary<string, string> Answers { get; set; } = new(StringComparer.Ordinal);
}
