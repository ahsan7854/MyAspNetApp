namespace MyApp.Web.Models;

public static class Graduate485NoForm80Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "graduate485NoForm80";
    public const string Title = "Graduate (Subclass 485) Visa Questionnaire - No Form 80";

    private static readonly (string Key, string Label)[] CharacterQuestions =
    [
        ("criminalConviction", "Has any applicant ever been convicted of an offence in any country (including any conviction which is now removed from official records)? If in doubt, click Yes."),
        ("pendingCharge", "Has any applicant ever been charged with any offence in any country that is currently awaiting legal action? If in doubt, click Yes."),
        ("protectionOrder", "Has any applicant who is included in this application ever been the subject of a domestic violence or family violence order, or any other order of a tribunal or court or other similar authority, for the personal protection of another person?"),
        ("arrestWarrant", "Has any applicant who is to be included in this application been the subject of an arrest warrant or Interpol Notice?"),
        ("childSexualOffence", "Has any applicant been found guilty of a sexually based offence involving a child (including where no conviction was recorded)?"),
        ("sexOffenderRegister", "Has any applicant who is to be included in this application ever been named on a sex offender register?"),
        ("courtConfinement", "Has any applicant been confined in a prison or psychiatric institution by order of a court in relation to criminal proceedings?"),
        ("unsoundMindAcquittal", "Has any applicant ever been acquitted of any offence on the grounds of unsoundness of mind or insanity? If in doubt, click yes."),
        ("unfitToPlead", "Has any applicant who is to be included in this application ever been found by a court not fit to plead?"),
        ("falseInformation", "Has any applicant ever provided any information or a document to the Australian Immigration or Customs Authorities which was wrong, incorrect, false or misleading?"),
        ("visaRefusal", "Has any applicant ever had a visa or entry permit for any country (including Australia) refused?"),
        ("visaOverstay", "Has any applicant overstayed a visa or entry permit in any country (including Australia)?"),
        ("deported", "Has any applicant been removed or deported from any country (including Australia)?"),
        ("avoidedDeportation", "Has any applicant left any country to avoid being removed or deported from that Country (including Australia)?"),
        ("excludedFromCountry", "Has any applicant been excluded from or asked to leave any country (including Australia)?"),
        ("citizenshipRefused", "Has any applicant ever been refused, renounced or rescinded citizenship of any country?"),
        ("internationalCrimes", "Has any applicant been charged with, or indicted for: genocide, war crimes, crimes against humanity, torture, slavery, or any other crime that is otherwise of a serious international concern?"),
        ("nationalSecurityRisk", "Has any applicant been directly or indirectly involved in, or associated with, any activities that would represent a risk to Australian national security or any other country?"),
        ("governmentDebt", "Has any applicant ever had any outstanding debts to the Australian Government or any public authority in Australia?"),
        ("peopleSmuggling", "Has any applicant ever been involved in people smuggling or people trafficking offences? If in doubt, click Yes."),
        ("criminalAssociation", "Has any applicant been associated with a person, group or organisation that has/is involved in criminal conduct?"),
        ("violentOrganisation", "Has any applicant ever been associated with an organisation engaged in violence or engaged in acts of violence (including war, insurgency, freedom fighting, terrorism, protest) either overseas or in Australia?"),
        ("militaryTraining", "Has any applicant undergone any military/paramilitary training, been trained in weapons/explosives or in the manufacture of chemical/biological products?"),
        ("militaryService", "Has any applicant ever served in a military force, police force, state sponsored militia, private militia, secret police or intelligence agency?"),
        ("sponsorshipBenefit", "Has any person included in this application made or offered to make a payment or provide another benefit of any kind to another person or entity in return for the sponsorship, nomination or support for an Australian visa?")
    ];

    private static readonly (string Key, string Label)[] HealthConditions =
    [
        ("healthConditionBloodDisorder", "Blood Disorder"),
        ("healthConditionCancer", "Cancer"),
        ("healthConditionDiabetes", "Diabetes"),
        ("healthConditionDisability", "Disability (physical or intellectual)"),
        ("healthConditionHeart", "Heart Condition"),
        ("healthConditionHepatitis", "Hepatitis B or C"),
        ("healthConditionHiv", "HIV Infection (including AIDS)"),
        ("healthConditionKidney", "Kidney Disease (including Dialysis)"),
        ("healthConditionLiver", "Liver Disease"),
        ("healthConditionMentalIllness", "Mental Illness"),
        ("healthConditionPregnancy", "Pregnancy"),
        ("healthConditionRespiratory", "Respiratory Illness"),
        ("healthConditionOther", "Other")
    ];

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
        Graduate485Questionnaire.Sections
            .Select(section => section.Number switch
            {
                5 => section with
                {
                    Questions = section.Questions
                        .Where(question => question.Key is not "futureTravel" and not "futureTravelDetails"
                            and not "futureAddresses" and not "futureAddressesDetails")
                        .ToArray()
                },
                6 => WithTemplateHealthQuestions(section),
                7 => WithTemplateCharacterQuestions(section),
                8 => WithTemplateDeclaration(section),
                _ => section
            })
            .ToArray();

    private static FormQuestionnaireSection WithTemplateHealthQuestions(
        FormQuestionnaireSection section)
    {
        var questions = section.Questions
            .Where(question => question.Key is not "healthConditions" and not "healthConditionsDetails")
            .ToList();
        var tuberculosisIndex = questions.FindIndex(question => question.Key == "tuberculosis");
        questions.InsertRange(tuberculosisIndex, HealthConditions.Select(condition =>
            new FormQuestion(condition.Key, condition.Label, FormQuestionType.Checkbox)));

        return section with
        {
            Questions = questions
                .Select(question => question.Key switch
                {
                    "healthExaminations" => question with { Label = "healthExaminationDetails.answer" },
                    "hospitalHealthcare" => question with { Label = "healthCareDetails.answer" },
                    "healthcareWork" => question with { Label = "healthcareWorkerDetails.answer" },
                    "agedCareWork" => question with { Label = "agedCareWorkerDetails.answer" },
                    "childcareWork" => question with { Label = "childCareCenterDetails.answer" },
                    "classroomParticipation" => question with { Label = "classroomSituationDetails.answer" },
                    "tuberculosis" => question with
                    {
                        Label = "Has any applicant ever had or currently have Tuberculosis or had a chest X-ray which showed an abnormality?"
                    },
                    "tuberculosisContact" => question with
                    {
                        Label = "Has any applicant ever been in close contact at home or at work with a person who has had Tuberculosis?"
                    },
                    "mobilityAssistance" => question with
                    {
                        Label = "Does any applicant require assistance with mobility or care due to a Health Condition?"
                    },
                    "privateHealthInsurance" => question with
                    {
                        Label = "Does any applicant hold Private Health Insurance that will cover them during their stay in Australia?"
                    },
                    _ => question
                })
                .ToArray()
        };
    }

    private static FormQuestionnaireSection WithTemplateCharacterQuestions(
        FormQuestionnaireSection section)
    {
        var labels = CharacterQuestions.ToDictionary(
            question => question.Key,
            question => question.Label,
            StringComparer.Ordinal);

        return section with
        {
            Questions = section.Questions
                .Select(question => labels.TryGetValue(question.Key, out var label)
                    ? question with { Label = label }
                    : question)
                .ToArray()
        };
    }

    private static FormQuestionnaireSection WithTemplateDeclaration(
        FormQuestionnaireSection section) =>
        section with
        {
            Questions = section.Questions
                .Select(question => question.Key == "declarationConfirmed"
                    ? question with
                    {
                        Label = "I understand and accept the terms & conditions and I confirm that the information I have supplied is true and correct. I also authorise my agent to conduct any and all necessary enquires to determine my eligibility including performing any visa Entitlement Check with immigration authorities."
                    }
                    : question)
                .ToArray()
        };
}
