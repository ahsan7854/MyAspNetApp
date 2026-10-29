namespace MyApp.Web.Models;

public static class VisitorVisaQuestionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "visitorVisa";
    public const string Title = "Visitor Visa Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 6, 11, 16, 17, 18, 19, 20, 21, 22, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        CreateSections();

    private static IReadOnlyList<FormQuestionnaireSection> CreateSections()
    {
        var sections = new List<FormQuestionnaireSection>();
        foreach (var section in BusinessVisaQuestionnaire.Sections
                     .Where(section => IncludedBusinessSections.Contains(section.Number)))
        {
            var questions = section.Questions.ToList();
            switch (section.Number)
            {
                case 2:
                    questions.RemoveAll(question => question.Key is
                        "suburbOfBirth" or "chineseCommercialCode" or "chineseCommercialCodeDetails" or
                        "russianDescent" or "differentDateOfBirth" or "differentDateOfBirthDetails");
                    break;
                case 3:
                    questions.RemoveAll(question => question.Key is
                        "currentCitizenshipCountries" or "previousCitizenshipDetails");
                    break;
                case 4:
                    questions.RemoveAll(question => question.Key is
                        "passportDetails" or "identityDocumentDetails");
                    break;
                case 20:
                    questions = questions
                        .Select(question => question.Key == "australianVisaHistory"
                            ? question with
                            {
                                Label = "Has the applicant ever previously applied for or held a visa for Australia?"
                            }
                            : question)
                        .ToList();
                    break;
                case 21:
                    questions = questions
                        .Select(question => question.Key == "travelHistory"
                            ? question with
                            {
                                Label = "Has the applicant visited any country in the last five years?"
                            }
                            : question)
                        .ToList();
                    break;
                case 22:
                    questions = questions
                        .Select(question => question.Key == "futureTravel"
                            ? question with
                            {
                                Label = "Does the applicant have any proposed or booked travel to any country?"
                            }
                            : question)
                        .ToList();
                    break;
                case 24:
                    sections.Add(section with { Number = sections.Count + 1 });
                    sections.Add(Section(
                        "Hospital and Health Care",
                        LongText("hospitalHealthCareDetails", "healthCareDetails.answer",
                            "Hospital and Health Care (healthCareDetails.answer)")));
                    continue;
                case 26:
                    sections.Add(section with { Number = sections.Count + 1 });
                    sections.Add(Section(
                        "Childcare Details",
                        LongText("childcareDetails", "childCareCenterDetails.answer",
                            "Childcare Details for All Applicants")));
                    sections.Add(Section(
                        "Classroom Details",
                        LongText("classroomDetails", "classroomSituationDetails.answer",
                            "Classroom Details for All Applicants")));
                    continue;
                case 28:
                    questions.Add(Question(
                        "privateHealthInsurance",
                        "Does any applicant hold private health insurance that will cover them during their stay in Australia?",
                        FormQuestionType.YesNo,
                        "Health Insurance for All Applicants"));
                    break;
            }

            sections.Add(section with
            {
                Number = sections.Count + 1,
                Questions = questions
            });

            if (section.Number == 22)
            {
                sections.Add(Section(
                    "Intended Study, Events and Supporting Funds",
                    LongText("intendedStudyDetails", "intendedStudyDetails.answer",
                        "Intended Study for All Applicants"),
                    LongText("intendedEventDetails", "intendedEventDetails.answer",
                        "Events for All Applicants"),
                    LongText("supportingFundsDetails", "supportingFundsDetails.answer",
                        "Supporting Funds for All Applicants")));
            }
        }

        return sections;
    }

    private static FormQuestionnaireSection Section(
        string title,
        params FormQuestion[] questions) =>
        new(0, title, false, questions);

    private static FormQuestion LongText(string key, string label, string groupTitle) =>
        Question(key, label, FormQuestionType.LongText, groupTitle);

    private static FormQuestion Question(
        string key,
        string label,
        FormQuestionType type,
        string groupTitle) =>
        new(key, label, type, groupTitle);
}
