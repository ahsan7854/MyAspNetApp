namespace MyApp.Web.Models;

public static class Talent124858NoForm80Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "talent124858NoForm80";
    public const string Title = "Talent (124/858) Visa Questionnaire - No Form 80";

    private static readonly HashSet<int> ExcludedTalentSections = [20, 21, 30];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        Talent124858Questionnaire.Sections
            .Where(section => !ExcludedTalentSections.Contains(section.Number))
            .Select((section, index) => section with
            {
                Number = index + 1
            })
            .ToArray();
}
