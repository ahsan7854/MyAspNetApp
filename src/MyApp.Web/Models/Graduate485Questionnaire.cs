namespace MyApp.Web.Models;

public static class Graduate485Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "graduate485";
    public const string Title = "Graduate (Subclass 485) Visa Questionnaire";

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
    [
        Section(1, "Personal details",
            Y("mainApplicant", "Are you the main applicant?"),
            Q("title", "Title"),
            Q("familyName", "Family name"),
            Q("givenNames", "Given names"),
            Q("preferredName", "Preferred name"),
            Q("gender", "Gender"),
            D("dateOfBirth", "Date of birth"),
            Q("countryOfBirth", "Country of birth"),
            Q("birthSuburb", "Suburb or town/city of birth"),
            Q("birthState", "State/province of birth"),
            Q("maritalStatus", "Marital status"),
            Y("otherNames", "Have you used other names, aliases or different name spellings?"),
            L("otherNamesDetails", "If yes, provide the other names, aliases or spellings."),
            Y("chineseCommercialCode", "Do you use a Chinese Commercial Code for your name?"),
            Y("russianDescent", "Are you of Russian descent?"),
            Q("clientId", "Department of Home Affairs client ID, if any"),
            Y("differentDateOfBirth", "Have you ever used a different date of birth?"),
            L("differentDateOfBirthDetails", "If yes, provide details.")),
        Section(2, "Citizenship & identity",
            L("currentCitizenships", "Current citizenship(s)"),
            L("statelessDetails", "If stateless, explain how, when and why."),
            L("previousCitizenships", "Previous citizenship(s)"),
            L("passportDetails", "Current or previous passport/travel document details"),
            L("identityDocumentDetails", "Government-issued identity document/number details"),
            Y("permanentResidenceRights", "Do you have permanent residence rights in a country where you are not a citizen?"),
            L("permanentResidenceRightsDetails", "If yes, provide details.")),
        Section(3, "Contact details",
            L("residentialAddress", "Residential address"),
            Y("sameResidentialAddress", "Do all applicants live at the same residential address?"),
            Q("afterHoursCountryCode", "After-hours telephone — country code"),
            Q("afterHoursAreaCode", "After-hours telephone — area code"),
            Q("afterHoursNumber", "After-hours telephone — number"),
            Q("officeHoursCountryCode", "Office-hours telephone — country code"),
            Q("officeHoursAreaCode", "Office-hours telephone — area code"),
            Q("officeHoursNumber", "Office-hours telephone — number"),
            Q("mobileCountryCode", "Mobile — country code"),
            Q("mobileNumber", "Mobile — number"),
            Y("samePhoneNumbers", "Do all applicants share the same contact telephone numbers?"),
            E("emailAddress", "Email address"),
            Y("sameEmailAddress", "Do all applicants share the same email address?"),
            Q("postalAddress", "Postal address — street number/name or PO Box"),
            Q("postalAddressLine2", "Postal address — address line 2"),
            Q("postalCity", "Suburb/town/city"),
            Q("postalState", "State/province"),
            Q("postalPostcode", "Postcode"),
            Q("postalCountry", "Country"),
            Y("samePostalAddress", "Do all applicants share the same postal address?")),
        Section(4, "Employment, education & family",
            Y("currentlyEmployed", "Are you currently employed in a paid position?"),
            L("employmentDetails", "If yes, provide details."),
            Y("undertakenFurtherEducation", "Have you undertaken or enrolled in education above secondary level?"),
            L("educationDetails", "If yes, provide details."),
            L("occupationalRegistrations", "Occupational registrations, licences or professional memberships"),
            Y("skillsAssessment", "Have you applied for an Australian skills assessment?"),
            L("skillsAssessmentDetails", "If yes, provide details."),
            Y("englishMainLanguage", "Is English your main language?"),
            Y("englishTest", "Have you undertaken an English language test within the last 36 months?"),
            L("englishTestDetails", "If yes, provide details."),
            Y("hasChildren", "Do you or your spouse have children, stepchildren or adopted children?"),
            L("childrenDetails", "If yes, provide details.")),
        Section(5, "Visa & travel history",
            Y("australianVisaHistory", "Have you previously applied for or held an Australian visa?"),
            L("australianVisaHistoryDetails", "If yes, provide details."),
            Y("internationalTravel", "In the last 10 years, since turning 16, have you visited countries outside your usual country of residence?"),
            L("internationalTravelDetails", "If yes, provide details."),
            Y("futureTravel", "Do you have proposed or booked future overseas travel?"),
            L("futureTravelDetails", "If yes, provide details."),
            Y("futureAddresses", "Do you have any future residential addresses?"),
            L("futureAddressesDetails", "If yes, provide details.")),
        Section(6, "Health details — all applicants",
            L("healthExaminations", "Previous immigration health examinations, including HAP ID if available"),
            L("hospitalHealthcare", "Intended hospital or health care visits"),
            L("healthcareWork", "Intended work in a health care setting"),
            L("agedCareWork", "Intended work in aged care"),
            L("childcareWork", "Intended work or attendance at a childcare centre"),
            L("classroomParticipation", "Intended participation in a classroom setting"),
            Y("tuberculosis", "Has any applicant had tuberculosis or an abnormal chest X-ray?"),
            Y("tuberculosisContact", "Has any applicant had close contact with someone who had tuberculosis?"),
            Y("healthConditions", "Does any applicant have a blood disorder, cancer, diabetes, disability, heart condition, hepatitis B/C, HIV, kidney/liver disease, mental illness, pregnancy, respiratory illness or another condition requiring treatment or follow-up?"),
            L("healthConditionsDetails", "If yes, provide details."),
            Y("mobilityAssistance", "Does any applicant require assistance with mobility or care?"),
            L("mobilityAssistanceDetails", "If yes, provide details."),
            Y("privateHealthInsurance", "Does any applicant hold private health insurance covering their stay in Australia?"),
            L("privateHealthInsuranceDetails", "If yes, provide details.")),
        CharacterSection(7, "Character & immigration history — all applicants",
            ("policeClearance", "Has any applicant applied for a police clearance certificate in the last 12 months?"),
            ("immigrationDetention", "Has any applicant been in immigration detention, a refugee camp or a refugee centre?"),
            ("criminalConviction", "Has any applicant been convicted of an offence, including a conviction removed from official records?"),
            ("pendingCharge", "Has any applicant been charged with an offence that is awaiting legal action?"),
            ("protectionOrder", "Has any applicant been subject to a domestic/family violence order or another personal protection order?"),
            ("arrestWarrant", "Has any applicant been subject to an arrest warrant or Interpol notice?"),
            ("childSexualOffence", "Has any applicant been found guilty of a sexual offence involving a child, including where no conviction was recorded?"),
            ("sexOffenderRegister", "Has any applicant been named on a sex offender register?"),
            ("courtConfinement", "Has any applicant been confined in a prison or psychiatric institution by court order in connection with criminal proceedings?"),
            ("unsoundMindAcquittal", "Has any applicant been acquitted on grounds of unsoundness of mind or insanity?"),
            ("unfitToPlead", "Has any applicant been found by a court not fit to plead?"),
            ("falseInformation", "Has any applicant supplied incorrect, false or misleading information/documents to Australian Immigration or Customs authorities?"),
            ("visaRefusal", "Has any applicant had a visa or entry permit refused by any country?"),
            ("visaOverstay", "Has any applicant overstayed a visa or entry permit in any country?"),
            ("deported", "Has any applicant been removed or deported from any country?"),
            ("avoidedDeportation", "Has any applicant left a country to avoid removal or deportation?"),
            ("excludedFromCountry", "Has any applicant been excluded from or asked to leave a country?"),
            ("citizenshipRefused", "Has any applicant had citizenship refused, renounced or rescinded?"),
            ("internationalCrimes", "Has any applicant been charged with or indicted for genocide, war crimes, crimes against humanity, torture, slavery or another serious international crime?"),
            ("nationalSecurityRisk", "Has any applicant been involved in activities posing a national security risk?"),
            ("governmentDebt", "Has any applicant had outstanding debts to the Australian Government or an Australian public authority?"),
            ("peopleSmuggling", "Has any applicant been involved in people smuggling or trafficking?"),
            ("criminalAssociation", "Has any applicant been associated with a person, group or organisation involved in criminal conduct?"),
            ("violentOrganisation", "Has any applicant been associated with an organisation engaged in violence or personally engaged in acts of violence?"),
            ("militaryTraining", "Has any applicant undertaken military/paramilitary, weapons, explosives or chemical/biological manufacturing training?"),
            ("militaryService", "Has any applicant served in a military force, police force, militia, secret police or intelligence agency?"),
            ("sponsorshipBenefit", "Has any applicant offered or provided payment or another benefit in exchange for Australian visa sponsorship, nomination or support?")),
        Section(8, "Declaration",
            Y("declarationConfirmed", "I understand and accept the terms and conditions, confirm that the information supplied is true and correct, and authorise my agent to make necessary enquiries to determine my eligibility, including visa entitlement checks with immigration authorities. Do you confirm this declaration?"),
            D("dateSubmitted", "Date submitted"))
    ];

    private static FormQuestionnaireSection Section(
        int number,
        string title,
        params FormQuestion[] questions) =>
        new(number, title, false, questions);

    private static FormQuestionnaireSection CharacterSection(
        int number,
        string title,
        params (string Key, string Label)[] questions) =>
        Section(number, title, questions.SelectMany(question =>
            new[]
            {
                Y(question.Key, question.Label),
                L(question.Key + "Details", "If yes, provide details.")
            }).ToArray());

    private static FormQuestion[] CharacterQuestion(string key, string label) =>
    [
        Y(key, label),
        L(key + "Details", "If yes, provide details.")
    ];

    private static FormQuestion Q(string key, string label) =>
        new(key, label, FormQuestionType.Text);

    private static FormQuestion L(string key, string label) =>
        new(key, label, FormQuestionType.LongText);

    private static FormQuestion D(string key, string label) =>
        new(key, label, FormQuestionType.Date);

    private static FormQuestion E(string key, string label) =>
        new(key, label, FormQuestionType.Email);

    private static FormQuestion Y(string key, string label) =>
        new(key, label, FormQuestionType.YesNo);
}
