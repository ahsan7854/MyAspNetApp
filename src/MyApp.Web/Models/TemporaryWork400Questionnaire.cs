namespace MyApp.Web.Models;

public static class TemporaryWork400Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "temporaryWork400";
    public const string Title = "Temporary Work (400) Visa Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 7, 8, 9, 11, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 33];

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
            case 7:
                questions.RemoveAll(question => question.Key is
                    "undertakenStudies" or "educationDetails");
                questions.Add(new FormQuestion(
                    "relevantEducation",
                    "Have you completed any education, course, qualifications or training that is relevant to your proposed work/activity in Australia?",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "relevantSkills",
                    "visaTypeLogic.mainApplicant.skills.skillsExperience.instruction.hasSkillsRelevantToProposedActivity",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "relevantSkillsDetails",
                    "Provide details of the relevant skills.",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "australianExperience",
                    "visaTypeLogic.mainApplicant.skills.skillsExperience.instruction.hasAustralianExperienceRelevantToProposedActivity",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "australianExperienceDetails",
                    "Provide details of the relevant experience.",
                    FormQuestionType.LongText));
                break;
            case 9:
                questions = questions
                    .Select(question => question.Key == "travelHistory"
                        ? question with
                        {
                            Label = "Has the applicant visited any country in the last five years?"
                        }
                        : question)
                    .ToList();
                break;
            case 12:
                questions = questions
                    .Select(question => question.Key == "australianSponsorOrEmployment"
                        ? question with
                        {
                            Label = "Are you being sponsored by or will you be working for an Australian Business or Organisation?"
                        }
                        : question)
                    .ToList();
                break;
            case 17:
                questions = questions
                    .Select(question => question.Key == "futureTravel"
                        ? question with
                        {
                            Label = "Does the applicant have any proposed or booked travel to Australia?"
                        }
                        : question)
                    .ToList();
                break;
            case 18:
                questions.Add(new FormQuestion(
                    "intendedStudyDetails",
                    "intendedStudyDetails.answer",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "intendedEventDetails",
                    "intendedEventDetails.answer",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "australianSponsoredStudyDetails",
                    "australianSponsoredStudyDetails.answer",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "homeGovernmentSponsoredStudent",
                    "Have you or anyone included in this application currently, or have they previously been, a Home Government Sponsored student or recipient?",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "supportingFundsDetails",
                    "supportingFundsDetails.answer",
                    FormQuestionType.LongText));
                break;
        }

        return questions;
    }
}
