namespace MyApp.Web.Models;

public static class Talent124858Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "talent124858";
    public const string Title = "Talent (124/858) Visa Questionnaire";

    private static readonly HashSet<int> IncludedBusinessSections =
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33];

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
            case 6:
                questions.Add(new FormQuestion(
                    "usualOccupation",
                    "What is your usual occupation?",
                    FormQuestionType.Text));
                questions.Add(new FormQuestion(
                    "currentAnnualSalaryAud",
                    "Current annual salary (AUD)",
                    FormQuestionType.Number));
                questions.Add(new FormQuestion(
                    "occupationIndustry",
                    "What industry/professional sector is your occupation associated with?",
                    FormQuestionType.Text));
                break;
            case 7:
                questions.Add(new FormQuestion(
                    "internationallyRecognisedTalent",
                    "Are you internationally recognised as being outstanding in your field?",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "distinguishedTalentField",
                    "What is your field of distinguished talent?",
                    FormQuestionType.Text));
                questions.Add(new FormQuestion(
                    "talentProfessionDescription",
                    "Give a brief description of the profession, arts, sport or research and academia in which you are involved.",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "stillProminentInField",
                    "Are you still prominent in your field?",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "currentProminenceDetails",
                    "Provide details of your current prominence in your chosen field.",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "settlementAssetToAustralianCommunity",
                    "visaTypeLogic.mainApplicant.skills.outstandingTalent.instruction.isSettlementAssetToAustralianCommunity",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "settlementAssetToCommunityDetails",
                    "visaTypeLogic.mainApplicant.skills.outstandingTalent.instruction.settlementAssetToCommunityDetails",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "employmentAndEstablishmentDifficult",
                    "visaTypeLogic.mainApplicant.skills.outstandingTalent.instruction.isEmploymentAndEstablishmentDifficult",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "employmentAndEstablishmentDetails",
                    "visaTypeLogic.mainApplicant.skills.outstandingTalent.instruction.employmentAndEstablishmentDetails",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "occupationAssociatedWithAustralianPrioritySector",
                    "visaTypeLogic.mainApplicant.skills.outstandingTalent.instruction.isOccupationAssociatedWithAustralianPrimarySector",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "australianPrioritySector",
                    "Specify which priority sector your occupation is associated with.",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "skillsRelevantToProposedActivity",
                    "visaTypeLogic.mainApplicant.skills.skillsExperience.instruction.hasSkillsRelevantToProposedActivity",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "relevantSkillsDetails",
                    "Provide details of the relevant skills.",
                    FormQuestionType.LongText));
                questions.Add(new FormQuestion(
                    "australianExperienceRelevantToProposedActivity",
                    "visaTypeLogic.mainApplicant.skills.skillsExperience.instruction.hasAustralianExperienceRelevantToProposedActivity",
                    FormQuestionType.YesNo));
                questions.Add(new FormQuestion(
                    "relevantAustralianExperienceDetails",
                    "Provide details of the relevant experience.",
                    FormQuestionType.LongText));
                break;
            case 15:
                questions = questions
                    .Select(question => question.Key == "australianSponsorOrEmployment"
                        ? question with
                        {
                            Label = "Have you received an offer of employment in Australia?"
                        }
                        : question)
                    .ToList();
                break;
            case 21:
                questions = questions
                    .Select(question => question.Key == "travelHistory"
                        ? question with
                        {
                            Label = "In the last 10 years, has the applicant visited any country outside their usual country of residence since turning 16 years of age?"
                        }
                        : question)
                    .ToList();
                break;
        }

        return questions;
    }
}
