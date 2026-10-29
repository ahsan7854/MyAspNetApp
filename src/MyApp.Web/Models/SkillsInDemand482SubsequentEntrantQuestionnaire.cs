namespace MyApp.Web.Models;

public static class SkillsInDemand482SubsequentEntrantQuestionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "skillsInDemand482SubsequentEntrant";
    public const string Title = "Skills in Demand (482) Subsequent Entrant Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 11, 16, 17, 18, 19, 24, 25, 26, 27, 28, 29, 30, 31, 33];

    private static readonly HashSet<string> IncludedPersonalDetails =
        ["title", "familyName", "givenNames", "preferredNames", "gender", "dateOfBirth", "maritalStatus"];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        BusinessVisaQuestionnaire.Sections
            .Where(section => IncludedBusinessSections.Contains(section.Number))
            .Select((section, index) => section with
            {
                Number = index + 1,
                Questions = section.Number switch
                {
                    2 => section.Questions
                        .Where(question => IncludedPersonalDetails.Contains(question.Key))
                        .ToArray(),
                    11 => section.Questions
                        .Where(question => question.Key is "hasChildren" or "childrenDetails")
                        .ToArray(),
                    _ => section.Questions
                }
            })
            .ToArray();
}
