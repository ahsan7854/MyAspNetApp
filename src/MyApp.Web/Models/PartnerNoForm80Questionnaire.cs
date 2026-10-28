namespace MyApp.Web.Models;

public static class PartnerNoForm80Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "partnerVisaNoForm80";
    public const string Title = "Partner Visa Questionnaire - No Form 80";

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } = CreateSections();

    private static IReadOnlyList<FormQuestionnaireSection> CreateSections()
    {
        var parentSections = ParentPermanentQuestionnaire.Sections;
        var employmentEducationAndFamily = parentSections.Single(section => section.Number == 3);
        var contactDetails = parentSections.Single(section => section.Number == 4);
        var visaHistory = parentSections.Single(section => section.Number == 5);
        var healthDetails = parentSections.Single(section => section.Number == 6);
        var characterHistory = parentSections.Single(section => section.Number == 7);
        var personalContacts = parentSections.Single(section => section.Number == 8);
        var declaration = parentSections.Single(section => section.Number == 9);

        return
        [
            parentSections[0],
            parentSections[1],
            employmentEducationAndFamily with
            {
                Number = 3,
                Title = "Education & language",
                Questions = employmentEducationAndFamily.Questions
                    .Where(question => question.Key is "undertakenFurtherEducation" or "englishMainLanguage")
                    .ToArray()
            },
            employmentEducationAndFamily with
            {
                Number = 4,
                Title = "Children & relationships",
                Questions = employmentEducationAndFamily.Questions
                    .Where(question => question.Key is "hasChildren" or "previousRelationship")
                    .Append(new FormQuestion(
                        "genuineDeFactoRelationship",
                        "Are you and your partner in a genuine, ongoing de facto relationship together to the exclusion of all others?",
                        FormQuestionType.YesNo))
                    .ToArray()
            },
            contactDetails with { Number = 5 },
            visaHistory with
            {
                Number = 6,
                Questions = visaHistory.Questions
                    .Select(question => question.Key == "internationalTravel"
                        ? question with { Label = "Have you visited any country in the last 10 years?" }
                        : question)
                    .ToArray()
            },
            healthDetails with { Number = 7 },
            characterHistory with { Number = 8 },
            personalContacts with { Number = 9 },
            declaration with { Number = 10 }
        ];
    }
}
