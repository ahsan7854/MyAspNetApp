namespace MyApp.Web.Models;

public static class Form80Over16Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "form80Over16";
    public const string Title = "Form 80 Questionnaire for Over 16 Applicants";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 5, 6, 7, 11, 15, 16, 17, 18, 19, 20, 21, 22, 23, 29, 30, 31, 32, 33];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        BusinessVisaQuestionnaire.Sections
            .Where(section => IncludedBusinessSections.Contains(section.Number))
            .Select((section, index) => section with
            {
                Number = index + 1,
                Questions = CustomizeQuestions(section)
            })
            .ToArray();

    private static IReadOnlyList<FormQuestion> CustomizeQuestions(
        FormQuestionnaireSection section)
    {
        var questions = section.Questions.ToList();
        switch (section.Number)
        {
            case 2:
                questions.RemoveAll(question => question.Key is
                    "chineseCommercialCode" or "chineseCommercialCodeDetails" or "russianDescent");
                break;
            case 3:
                questions.RemoveAll(question => question.Key is
                    "currentCitizenshipCountries" or "previousCitizenshipDetails");
                break;
            case 4:
                questions.RemoveAll(question => question.Key is
                    "passportDetails" or "identityDocumentDetails");
                break;
            case 7:
                questions = questions
                    .Select(question => question.Key == "undertakenStudies"
                        ? question with
                        {
                            Label = "Have you ever undertaken or enrolled in any education or course above secondary level? Include college/vocational schools, university, research/thesis, specialised training and skill/trade qualifications."
                        }
                        : question)
                    .ToList();
                break;
            case 15:
                questions = questions
                    .Select(question => question.Key == "australianSponsorOrEmployment"
                        ? question with
                        {
                            Label = "Are you being sponsored by or will you be working for an Australian business or organisation?"
                        }
                        : question)
                    .ToList();
                break;
        }

        return questions;
    }
}
