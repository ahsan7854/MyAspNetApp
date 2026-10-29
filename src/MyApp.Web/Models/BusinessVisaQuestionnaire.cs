namespace MyApp.Web.Models;

public static class BusinessVisaQuestionnaire
{
    public const int CurrentVersion = 2;
    public const string Type = "businessVisa";
    public const string Title = "Business Visa Client Questionnaire";

    private static readonly IReadOnlyDictionary<string, string> QuestionGroups =
        CreateQuestionGroups();

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
    [
        Section(1, "Preparer Details", false,
            Y("mainApplicant", "Are you or will you be the main applicant in any application?")),
        Section(2, "Personal Details", true,
            Q("title", "Title or prefix"),
            Q("familyName", "Family name"),
            Q("givenNames", "Given names"),
            Q("preferredNames", "Preferred names"),
            Q("gender", "Gender"),
            D("dateOfBirth", "Date of birth"),
            Q("countryOfBirth", "Country of birth"),
            Q("suburbOfBirth", "Suburb of birth"),
            Q("cityOfBirth", "City or town of birth"),
            Q("stateOfBirth", "State or province of birth"),
            Q("maritalStatus", "What is your marital status?"),
            Y("otherNames", "Have you ever had or been known by any other name or alias, or used a different name spelling?"),
            L("otherNamesDetails", "If yes, provide details."),
            Y("chineseCommercialCode", "Do you use a Chinese Commercial Code for your name?"),
            L("chineseCommercialCodeDetails", "If yes, provide details."),
            Y("russianDescent", "Are you of Russian descent?"),
            Q("clientId", "Department of Immigration/Home Affairs Client ID"),
            Y("differentDateOfBirth", "Have you ever had a different date of birth?"),
            L("differentDateOfBirthDetails", "If yes, provide details.")),
        Section(3, "Citizenship", false,
            Y("currentlyCitizen", "Are you currently a citizen of any country?"),
            Q("currentCitizenshipCountries", "Country or countries"),
            L("statelessDetails", "If you are not a citizen of any country, explain how, when and why you are stateless."),
            Y("everCitizen", "Have you ever been a citizen of any country?"),
            L("previousCitizenshipDetails", "If yes, provide details.")),
        Section(4, "Passports and Identity Documents", false,
            Y("hasHeldPassport", "Do you currently hold or have you ever held a passport or travel document?"),
            L("passportDetails", "If yes, provide details."),
            Y("hasGovernmentIdentity", "Do you have or have you ever had a government-issued identity document or identity number?"),
            L("identityDocumentDetails", "If yes, provide details.")),
        Section(5, "Permanent Residency", false,
            Y("permanentResidenceRights", "Do you have the right to permanently reside in any country of which you are not a citizen?"),
            L("permanentResidenceDetails", "If yes, provide details.")),
        Section(6, "Employment", false,
            Y("currentlyEmployed", "Are you currently employed in a paid position?"),
            L("employmentDetails", "If yes, provide details.")),
        Section(7, "Education and Training", true,
            Y("undertakenStudies", "Have you ever undertaken or enrolled in any studies or training at secondary level or above? Include high school, college/vocational schools, university, research/thesis, specialised training and skill/trade qualifications."),
            L("educationDetails", "If yes, provide details.")),
        Section(8, "Occupational Registrations, Licences and Professional Memberships", true,
            Y("hasOccupationalRegistration", "Do you hold any occupational registrations, licences or professional memberships?"),
            L("occupationalRegistrationDetails", "If yes, provide details.")),
        Section(9, "Skills Assessments", false,
            Y("appliedForSkillsAssessment", "Have you applied for a skills assessment from an Australian skills assessing body?"),
            L("skillsAssessmentDetails", "If yes, provide details.")),
        Section(10, "Languages and English Tests", false,
            Y("englishMainLanguage", "Is English your main language?"),
            L("englishMainLanguageDetails", "If yes, provide details."),
            Y("recentEnglishTest", "Have you undertaken any English language test within the last 36 months?"),
            L("englishTestDetails", "If yes, provide details.")),
        Section(11, "Children", true,
            Y("hasChildren", "Do you or your spouse have any children, stepchildren or adoptive children?"),
            L("childrenDetails", "If yes, provide details.")),
        Section(12, "Business Ownership and Experience", true,
            Y("ownedBusiness", "Have you been an owner or part owner of a business during the previous five years?"),
            L("businessOwnershipDetails", "If yes, provide details."),
            N("businessExperienceYears", "How many years of experience do you have in business and/or investment?"),
            Y("registeredPatents", "Do you or any of your main businesses have any registered patents or registered designs?"),
            L("registeredPatentsDetails", "If yes, provide details."),
            Y("registeredTrademarks", "Do you or any of your main businesses have any registered trademarks?"),
            L("registeredTrademarksDetails", "If yes, provide details."),
            Y("jointVentureAgreements", "Do you or any of your main businesses have any joint venture agreements in place with other entities?"),
            L("jointVentureAgreementsDetails", "If yes, provide details."),
            Y("exportTrade", "Are you or any of your main businesses involved in export trade?"),
            L("exportTradeDetails", "If yes, provide details."),
            Y("innovationGrants", "Have you or any of your main businesses received any funding grants for innovation?"),
            L("innovationGrantsDetails", "If yes, provide details."),
            Y("ventureCapitalFunding", "Have you or any of your main businesses received any venture capital funding?"),
            L("ventureCapitalFundingDetails", "If yes, provide details."),
            Y("businessLosses", "Have any of your businesses operated at a loss while you were involved with them?"),
            L("businessLossesDetails", "If yes, provide details."),
            Y("insolvencyInvolvement", "Have you ever been, or are you currently, involved in a business that has been or is currently subject to insolvency, receivership or liquidation?"),
            L("insolvencyInvolvementDetails", "If yes, provide details."),
            Y("bankruptcy", "Have you ever experienced, or are you currently experiencing, bankruptcy?"),
            L("bankruptcyDetails", "If yes, provide details.")),
        Section(13, "Assets and Liabilities", false,
            D("financialPeriodYear1EndDate", "Year 1 financial period end date"),
            D("financialPeriodYear2EndDate", "Year 2 financial period end date")),
        Section(14, "Business and Financial Intentions", false,
            Y("seekingAustralianBusinessOrInvestmentVisa", "Are you seeking to obtain an Australian visa for the purpose of owning an Australian business or investing in Australia?"),
            Q("netAssetsAvailableForTransferAud", "Value of net assets available for transfer to Australia within two years (AUD)"),
            Q("netAssetsIntendedForTransferAud", "Value of net assets you intend to transfer to Australia within two years of the grant of any visa (AUD)"),
            Q("intendedComplyingInvestmentAud", "Amount you intend, or would be able, to invest in a complying investment in Australia (AUD)"),
            L("businessInvestmentIntentions", "Provide a brief overview of your business and/or investment intentions in Australia.")),
        Section(15, "Australian Employment or Sponsorship", false,
            Y("australianSponsorOrEmployment", "Are you being sponsored by, or will you be working for, an Australian business or organisation?"),
            L("australianSponsorOrEmploymentDetails", "If yes, provide details.")),
        Section(16, "Residential Addresses", false,
            Y("sameResidentialAddress", "Does every applicant included in this application currently live at the same residential address?"),
            L("residentialAddressDetails", "If no, provide details.")),
        Section(17, "Contact Phone Numbers", true,
            Y("samePhoneNumbers", "Does everyone included in this application share the same contact phone numbers?"),
            Q("phoneApplicantName", "Applicant's name"),
            D("phoneApplicantDateOfBirth", "Applicant's date of birth"),
            Q("afterHoursCountryCode", "After-hours phone number — country code"),
            Q("afterHoursAreaCode", "After-hours phone number — area code"),
            Q("afterHoursNumber", "After-hours phone number — number"),
            Q("officeHoursCountryCode", "Office-hours phone number — country code"),
            Q("officeHoursAreaCode", "Office-hours phone number — area code"),
            Q("officeHoursNumber", "Office-hours phone number — number"),
            Q("mobileCountryCode", "Mobile phone number — country code"),
            Q("mobileNumber", "Mobile phone number — number")),
        Section(18, "Email Addresses", true,
            Y("sameEmailAddress", "Does everyone included in this application share the same email address?"),
            Q("emailApplicantName", "Applicant's name"),
            E("applicantEmail", "Email address")),
        Section(19, "Postal Addresses", true,
            Y("samePostalAddress", "Does everyone included in this application share the same postal address?"),
            Q("postalApplicantName", "Applicant's name"),
            L("postalStreetAddress", "Street number and name / Post Office Box"),
            Q("postalAddressLine2", "Address line 2"),
            Q("postalSuburb", "Suburb/town/city"),
            Q("postalState", "State"),
            Q("postalPostcode", "Postcode"),
            Q("postalCountry", "Country")),
        Section(20, "Australian Visa History", false,
            Y("australianVisaHistory", "Have you ever previously applied for or held a visa for Australia?"),
            L("australianVisaHistoryDetails", "If yes, provide details.")),
        Section(21, "Travel History", false,
            Y("travelHistory", "In the last ten years, have you visited any country outside your usual country of residence since turning 16 years of age?"),
            L("travelHistoryDetails", "If yes, provide details.")),
        Section(22, "Future Travel", false,
            Y("futureTravel", "Do you have any proposed or booked travel to any country?"),
            L("futureTravelDetails", "If yes, provide details.")),
        Section(23, "Future Address", false,
            L("futureAddressResponse", "futureAddress.hasFutureAddresses")),
        Section(24, "Health Examinations", false,
            L("healthExaminationResponse", "healthExaminationDetails.answer")),
        Section(25, "Health Care Work", false,
            L("healthCareWorkResponse", "healthcareWorkerDetails.answer")),
        Section(26, "Aged Care Work", false,
            L("agedCareWorkResponse", "agedCareWorkerDetails.answer")),
        Section(27, "Tuberculosis", false,
            Y("tuberculosisHistory", "Has any applicant ever had or currently have tuberculosis, or had a chest X-ray that showed an abnormality?"),
            L("tuberculosisHistoryDetails", "If yes, provide the applicant's name and details."),
            Y("tuberculosisContact", "Has any applicant ever been in close contact at home or at work with a person who has had tuberculosis?"),
            L("tuberculosisContactDetails", "If yes, provide the applicant's name and details.")),
        Section(28, "Health Conditions", false,
            Y("healthConditions", "Does any applicant have any of the following health conditions that may incur medical costs, require treatment or require medical follow up?"),
            C("healthConditionBloodDisorder", "Blood Disorder"),
            C("healthConditionCancer", "Cancer"),
            C("healthConditionDiabetes", "Diabetes"),
            C("healthConditionDisability", "Disability (physical or intellectual)"),
            C("healthConditionHeart", "Heart Condition"),
            C("healthConditionHepatitis", "Hepatitis B or C"),
            C("healthConditionHiv", "HIV Infection (including AIDS)"),
            C("healthConditionKidney", "Kidney Disease (including Dialysis)"),
            C("healthConditionLiver", "Liver Disease"),
            C("healthConditionMentalIllness", "Mental Illness"),
            C("healthConditionPregnancy", "Pregnancy"),
            C("healthConditionRespiratory", "Respiratory Illness"),
            C("healthConditionOther", "Other"),
            L("healthConditionDetails", "Provide the applicant's name and details."),
            Y("mobilityAssistance", "Does any applicant require assistance with mobility or care due to a health condition?"),
            L("mobilityAssistanceDetails", "If yes, provide the applicant's name and details.")),
        Section(29, "Police Clearances", false,
            Y("policeClearance", "Has anyone included in this application applied for a police clearance certificate in the last 12 months?"),
            L("policeClearanceDetails", "If yes, provide details.")),
        Section(30, "Immigration Detention or Refugee Camp History", false,
            Y("detentionOrRefugeeCamp", "Has anyone included in this application previously been in immigration detention, a refugee camp or a centre for refugees?"),
            L("detentionOrRefugeeCampDetails", "If yes, provide details.")),
        CharacterSection(),
        Section(32, "Family Contacts in Australia", true,
            Y("familyContactsInAustralia", "Do you have any family members—parents, siblings or children—in Australia who have not already been listed in this questionnaire?"),
            L("familyContactsInAustraliaDetails", "If yes, provide details.")),
        Section(33, "Declaration", false,
            L("declarationText", "I understand and accept the terms & conditions and I confirm that the information I have supplied is true and correct. I also authorise my agent to conduct any and all necessary enquires to determine my eligibility including performing any visa Entitlement Check with immigration authorities."),
            Y("declarationConfirmed", "I confirm and accept the above declaration."),
            D("dateSubmitted", "Date submitted"))
    ];

    private static FormQuestionnaireSection CharacterSection()
    {
        var declarations = new (string Key, string Label)[]
        {
            ("criminalConviction", "Has any applicant ever been convicted of an offence in any country (including any conviction which is now removed from official records)? If in doubt, click Yes."),
            ("pendingCharge", "Has any applicant ever been charged with any offence in any country that is currently awaiting legal action? If in doubt, click Yes."),
            ("protectionOrder", "Has any applicant who is included in this application ever been the subject of a domestic violence or family violence order, or any other order of a tribunal or court or other similar authority, for the personal protection of another person?"),
            ("arrestWarrant", "Has any applicant who is to be included in this application been the subject of an arrest warrant or Interpol Notice?"),
            ("childSexualOffence", "Has any applicant been found guilty of a sexually based offence involving a child (including where no conviction was recorded)?"),
            ("sexOffenderRegister", "Has any applicant who is to be included in this application ever been named on a sex offender register?"),
            ("courtConfinement", "Has any applicant been confined in a prison or psychiatric institution by order of a court in relation to criminal proceedings?"),
            ("unsoundMindAcquittal", "Has any applicant ever been acquitted of any offence on the grounds of unsoundness of mind or insanity? If in doubt, click yes."),
            ("unfitToPlead", "Has any applicant who is to be included in this application ever been found by a court not fit to plead?"),
            ("falseImmigrationInformation", "Has any applicant ever provided any information or a document to the Australian Immigration or Customs Authorities which was wrong, incorrect, false or misleading?"),
            ("visaRefusal", "Has any applicant ever had a visa or entry permit for any country (including Australia) refused?"),
            ("visaOverstay", "Has any applicant overstayed a visa or entry permit in any country, including Australia?"),
            ("deportedOrRemoved", "Has any applicant been removed or deported from any country, including Australia?"),
            ("leftToAvoidRemoval", "Has any applicant left any country to avoid being removed or deported from that Country (including Australia)?"),
            ("excludedFromCountry", "Has any applicant been excluded from or asked to leave any country (including Australia)?"),
            ("citizenshipRefusedOrRenounced", "Has any applicant ever been refused, renounced or rescinded citizenship of any country?"),
            ("internationalCrimes", "Has any applicant been charged with, or indicted for: genocide, war crimes, crimes against humanity, torture, slavery, or any other crime that is otherwise of a serious international concern?"),
            ("nationalSecurityRisk", "Has any applicant been directly or indirectly involved in, or associated with, any activities that would represent a risk to Australian national security or any other country?"),
            ("australianGovernmentDebts", "Has any applicant ever had any outstanding debts to the Australian Government or any public authority in Australia?"),
            ("peopleSmugglingOrTrafficking", "Has any applicant ever been involved in people smuggling or people trafficking offences? If in doubt, click Yes."),
            ("criminalAssociation", "Has any applicant been associated with a person, group or organisation that has/is involved in criminal conduct?"),
            ("violenceOrTerrorism", "Has any applicant ever been associated with an organisation engaged in violence or engaged in acts of violence (including war, insurgency, freedom fighting, terrorism, protest) either overseas or in Australia?"),
            ("militaryOrWeaponsTraining", "Has any applicant undergone any military/paramilitary training, been trained in weapons/explosives or in the manufacture of chemical/biological products?"),
            ("militaryPoliceOrIntelligenceService", "Has any applicant ever served in a military force, police force, state-sponsored militia, private militia, secret police or intelligence agency?"),
            ("visaSponsorshipBenefit", "Has any person included in this application made or offered to make a payment or provide another benefit of any kind to another person or entity in return for the sponsorship, nomination or support for an Australian visa?")
        };

        return Section(31, "Character and Immigration Declarations", false,
            declarations.SelectMany(declaration => new[]
            {
                Y(declaration.Key, declaration.Label),
                L(declaration.Key + "Details", "If yes, provide the applicant's name and full details.")
            }).ToArray());
    }

    private static FormQuestionnaireSection Section(
        int number,
        string title,
        bool repeatable,
        params FormQuestion[] questions) =>
        new(number, title, repeatable, questions
            .Select(question => QuestionGroups.TryGetValue(question.Key, out var groupTitle)
                ? question with { GroupTitle = groupTitle }
                : question)
            .ToArray());

    private static IReadOnlyDictionary<string, string> CreateQuestionGroups()
    {
        var groups = new Dictionary<string, string>(StringComparer.Ordinal);

        AddGroup(groups, "Preparer Details", "mainApplicant");
        AddGroup(groups, "Personal Details for the Applicant",
            "title", "familyName", "givenNames", "preferredNames", "gender", "dateOfBirth",
            "countryOfBirth", "suburbOfBirth", "cityOfBirth", "stateOfBirth", "maritalStatus");
        AddGroup(groups, "Other Names for the Applicant", "otherNames");
        AddGroup(groups, "Other Details for the Applicant", "otherNamesDetails", "chineseCommercialCode",
            "chineseCommercialCodeDetails", "russianDescent", "clientId");
        AddGroup(groups, "Other Dates of Birth for the Applicant", "differentDateOfBirth",
            "differentDateOfBirthDetails");
        AddGroup(groups, "Citizenships for the Applicant", "currentlyCitizen", "currentCitizenshipCountries",
            "statelessDetails", "everCitizen", "previousCitizenshipDetails");
        AddGroup(groups, "Passports for the Applicant", "hasHeldPassport", "passportDetails");
        AddGroup(groups, "Identity Documents for the Applicant", "hasGovernmentIdentity",
            "identityDocumentDetails");
        AddGroup(groups, "Permanent Residency", "permanentResidenceRights", "permanentResidenceDetails");
        AddGroup(groups, "Employment for the Applicant", "currentlyEmployed", "employmentDetails");
        AddGroup(groups, "Education for the Applicant", "undertakenStudies", "educationDetails");
        AddGroup(groups, "Occupational Registrations, Licences and Professional Memberships for the Applicant",
            "hasOccupationalRegistration", "occupationalRegistrationDetails");
        AddGroup(groups, "Skill Assessments Applied for by the Applicant", "appliedForSkillsAssessment",
            "skillsAssessmentDetails");
        AddGroup(groups, "Languages for the Applicant", "englishMainLanguage", "englishMainLanguageDetails");
        AddGroup(groups, "English Tests for the Applicant", "recentEnglishTest", "englishTestDetails");
        AddGroup(groups, "Children", "hasChildren", "childrenDetails");
        AddGroup(groups, "Business Ownership", "ownedBusiness", "businessOwnershipDetails");
        AddGroup(groups, "Business Skills", "businessExperienceYears", "businessLosses",
            "businessLossesDetails");
        AddGroup(groups, "Enter Details of Registered Patents and Registered Designs", "registeredPatents",
            "registeredPatentsDetails");
        AddGroup(groups, "Enter Details of Registered Trademarks", "registeredTrademarks",
            "registeredTrademarksDetails");
        AddGroup(groups, "Enter Details of Joint Venture Agreements", "jointVentureAgreements",
            "jointVentureAgreementsDetails");
        AddGroup(groups, "Enter Details of Export Trade", "exportTrade", "exportTradeDetails");
        AddGroup(groups, "Enter Details of Funding Grants Received for Innovation", "innovationGrants",
            "innovationGrantsDetails");
        AddGroup(groups, "Enter Details of Venture Capital Funding Received", "ventureCapitalFunding",
            "ventureCapitalFundingDetails");
        AddGroup(groups, "Insolvency, Receivership or Liquidation", "insolvencyInvolvement",
            "insolvencyInvolvementDetails");
        AddGroup(groups, "Bankruptcy", "bankruptcy", "bankruptcyDetails");
        AddGroup(groups, "Year 1 Assets and Liabilities", "financialPeriodYear1EndDate");
        AddGroup(groups, "Year 2 Assets and Liabilities", "financialPeriodYear2EndDate");
        AddGroup(groups, "Business / Financial Intentions", "seekingAustralianBusinessOrInvestmentVisa",
            "netAssetsAvailableForTransferAud", "netAssetsIntendedForTransferAud",
            "intendedComplyingInvestmentAud", "businessInvestmentIntentions");
        AddGroup(groups, "Employment", "australianSponsorOrEmployment",
            "australianSponsorOrEmploymentDetails");
        AddGroup(groups, "All Applicants Addresses", "sameResidentialAddress", "residentialAddressDetails");
        AddGroup(groups, "Phone Numbers for All Applicants", "samePhoneNumbers", "phoneApplicantName",
            "phoneApplicantDateOfBirth", "afterHoursCountryCode", "afterHoursAreaCode", "afterHoursNumber",
            "officeHoursCountryCode", "officeHoursAreaCode", "officeHoursNumber", "mobileCountryCode",
            "mobileNumber");
        AddGroup(groups, "Email Addresses for All Applicants", "sameEmailAddress", "emailApplicantName",
            "applicantEmail");
        AddGroup(groups, "Postal Addresses for All Applicants", "samePostalAddress", "postalApplicantName",
            "postalStreetAddress", "postalAddressLine2", "postalSuburb", "postalState", "postalPostcode",
            "postalCountry");
        AddGroup(groups, "Visas for the Applicant", "australianVisaHistory", "australianVisaHistoryDetails");
        AddGroup(groups, "Travel History for the Applicant", "travelHistory", "travelHistoryDetails");
        AddGroup(groups, "Future Travel for the Applicant", "futureTravel", "futureTravelDetails");
        AddGroup(groups, "Future Address", "futureAddressResponse");
        AddGroup(groups, "Health Examinations for All Applicants", "healthExaminationResponse");
        AddGroup(groups, "Health Care Work (healthcareWorkerDetails.answer)", "healthCareWorkResponse");
        AddGroup(groups, "Aged Care Work (agedCareWorkerDetails.answer)", "agedCareWorkResponse");
        AddGroup(groups, "Tuberculosis", "tuberculosisHistory", "tuberculosisHistoryDetails");
        AddGroup(groups, "Tuberculosis Exposure", "tuberculosisContact", "tuberculosisContactDetails");
        AddGroup(groups, "Health Conditions for All Applicants", "healthConditions",
            "healthConditionBloodDisorder", "healthConditionCancer", "healthConditionDiabetes",
            "healthConditionDisability", "healthConditionHeart", "healthConditionHepatitis",
            "healthConditionHiv", "healthConditionKidney", "healthConditionLiver",
            "healthConditionMentalIllness", "healthConditionPregnancy", "healthConditionRespiratory",
            "healthConditionOther", "healthConditionDetails");
        AddGroup(groups, "Mobility or Care Details", "mobilityAssistance", "mobilityAssistanceDetails");
        AddGroup(groups, "Police Clearances for All Applicants", "policeClearance", "policeClearanceDetails");
        AddGroup(groups, "Immigration Detention / Refugee Camp History for All Applicants",
            "detentionOrRefugeeCamp", "detentionOrRefugeeCampDetails");

        var characterGroups = new (string Title, string Key)[]
        {
            ("Convictions for Crimes/Offences", "criminalConviction"),
            ("Offences Awaiting Legal Action", "pendingCharge"),
            ("Domestic Violence Order", "protectionOrder"),
            ("Arrest Warrants/Interpol Notices", "arrestWarrant"),
            ("Sexual Offences Involving a Child", "childSexualOffence"),
            ("Sex Offender Details", "sexOffenderRegister"),
            ("Prison or Psychiatric Confinement", "courtConfinement"),
            ("Acquittals due to Mental Illness", "unsoundMindAcquittal"),
            ("Not Fit to Plead", "unfitToPlead"),
            ("Incorrect Information to Australian Immigration", "falseImmigrationInformation"),
            ("Refused Visas", "visaRefusal"),
            ("Overstayed Visas", "visaOverstay"),
            ("Deportations/Removals", "deportedOrRemoved"),
            ("Left to Avoid Deportation", "leftToAvoidRemoval"),
            ("Exclusions", "excludedFromCountry"),
            ("Refused Citizenships", "citizenshipRefusedOrRenounced"),
            ("Genocide, War Crimes etc.", "internationalCrimes"),
            ("Risk to National Security", "nationalSecurityRisk"),
            ("Debts to Governments", "australianGovernmentDebts"),
            ("Illegal Movement of People", "peopleSmugglingOrTrafficking"),
            ("Involvement with Criminal Conduct", "criminalAssociation"),
            ("Violence", "violenceOrTerrorism"),
            ("Military Training for All Applicants", "militaryOrWeaponsTraining"),
            ("Military Service for All Applicants", "militaryPoliceOrIntelligenceService"),
            ("Payment for Visa Sponsorship", "visaSponsorshipBenefit")
        };
        foreach (var (title, key) in characterGroups)
        {
            AddGroup(groups, title, key, key + "Details");
        }

        AddGroup(groups, "Personal Contacts for the Applicant", "familyContactsInAustralia",
            "familyContactsInAustraliaDetails");
        AddGroup(groups, "Declaration", "declarationText", "declarationConfirmed", "dateSubmitted");
        return groups;
    }

    private static void AddGroup(
        IDictionary<string, string> groups,
        string title,
        params string[] questionKeys)
    {
        foreach (var questionKey in questionKeys)
        {
            groups.Add(questionKey, title);
        }
    }

    private static FormQuestion Q(string key, string label) =>
        new(key, label, FormQuestionType.Text);

    private static FormQuestion L(string key, string label) =>
        new(key, label, FormQuestionType.LongText);

    private static FormQuestion D(string key, string label) =>
        new(key, label, FormQuestionType.Date);

    private static FormQuestion N(string key, string label) =>
        new(key, label, FormQuestionType.Number);

    private static FormQuestion C(string key, string label) =>
        new(key, label, FormQuestionType.Checkbox);

    private static FormQuestion E(string key, string label) =>
        new(key, label, FormQuestionType.Email);

    private static FormQuestion Y(string key, string label) =>
        new(key, label, FormQuestionType.YesNo);
}
