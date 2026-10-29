namespace MyApp.Web.Models;

public static class SkillsInDemand482Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "skillsInDemand482";
    public const string Title = "Skills in Demand (482) Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 6, 7, 8, 9, 10, 11, 15, 16, 17, 18, 19, 20, 21, 24, 25, 26, 27, 28, 29, 30, 31, 33];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        BusinessVisaQuestionnaire.Sections
            .Where(section => IncludedBusinessSections.Contains(section.Number))
            .Select((section, index) => section with
            {
                Number = index + 1,
                Questions = section.Number == 21
                    ? section.Questions
                        .Select(question => question.Key == "travelHistory"
                            ? question with
                            {
                                Label = "In the last 10 years, has the applicant visited any country outside their usual country of residence since turning 16 years of age?"
                            }
                            : question)
                        .ToArray()
                    : section.Questions
            })
            .ToArray();
}
