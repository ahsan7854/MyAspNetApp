namespace MyApp.Web.Models;

public static class PartnerQuestionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "partnerVisa";
    public const string Title = "Partner Visa Questionnaire";

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        ParentPermanentQuestionnaire.Sections
            .Select(section => section.Number == 3
                ? section with
                {
                    Title = "Employment, education, language, children & relationships",
                    Questions = section.Questions
                        .Take(4)
                        .Append(new FormQuestion(
                            "genuineDeFactoRelationship",
                            "Are you and your partner in a genuine, ongoing de facto relationship together to the exclusion of all others?",
                            FormQuestionType.YesNo))
                        .Concat(section.Questions.Skip(4))
                        .ToArray()
                }
                : section)
            .ToArray();
}
