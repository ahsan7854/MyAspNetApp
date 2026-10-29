namespace MyApp.Web.Models;

public static class Skilled189190Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "skilled189190";
    public const string Title = "Skilled (189/190) Visas Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        BusinessVisaQuestionnaire.Sections
            .Where(section => IncludedBusinessSections.Contains(section.Number))
            .Select((section, index) => section with { Number = index + 1 })
            .ToArray();
}
