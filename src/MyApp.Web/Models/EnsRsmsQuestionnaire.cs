namespace MyApp.Web.Models;

public static class EnsRsmsQuestionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "ensRsms";
    public const string Title = "ENS/RSMS Visa Questionnaire";

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        BusinessVisaQuestionnaire.Sections
            .Where(section => section.Number != 2)
            .Select((section, index) => section with { Number = index + 1 })
            .ToArray();
}
