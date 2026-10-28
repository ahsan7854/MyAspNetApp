namespace MyApp.Web.Models;

public static class ChildUnder18Questionnaire
{
    public const int CurrentVersion = 1;
    public const string Type = "childUnder18";
    public const string Title = "Child Visa Questionnaire (Under 18)";

    public static IReadOnlyList<FormQuestionnaireSection> Sections { get; } =
    [
        Section(1, "Personal details",
            Y("mainApplicant", "Are you or will you be the main applicant?"),
            Q("title", "Prefix/title"),
            Q("familyName", "Family name"),
            Q("givenNames", "Given names"),
            Q("preferredNames", "Preferred names"),
            Q("gender", "Gender"),
            D("dateOfBirth", "Date of birth"),
            Q("countryOfBirth", "Country of birth"),
            Q("birthSuburb", "Suburb of birth"),
            Q("birthCity", "City/town of birth"),
            Q("birthState", "State/province of birth"),
            Q("maritalStatus", "Marital status"),
            Y("otherNames", "Have you ever used another name, alias or different name spelling?"),
            Y("chineseCommercialCode", "Do you use a Chinese Commercial Code for your name?"),
            Y("russianDescent", "Are you of Russian descent?"),
            Y("hasClientId", "Do you have a Department of Home Affairs client ID?"),
            Y("differentDateOfBirth", "Have you ever had a different date of birth?")),
        Section(2, "Citizenship & identity",
            Y("currentlyCitizen", "Are you currently a citizen of any country?"),
            L("currentCitizenships", "List all current citizenship countries."),
            L("statelessDetails", "If stateless, explain how, when and why."),
            Y("previousCitizenship", "Have you previously been a citizen of any country?"),
            Y("hasPassport", "Do you currently hold or have you ever held a passport or travel document?"),
            Y("hasIdentityDocument", "Do you have or have you ever had a government-issued identity document or identity number?"),
            Y("permanentResidenceRights", "Do you have the right to permanently reside in a country of which you are not a citizen?")),
        Section(3, "Language & children",
            Y("englishMainLanguage", "Is English your main language?"),
            Y("hasChildren", "Do you or your spouse have children, stepchildren or adopted children?")),
        Section(4, "Contact details",
            Q("applicantName", "Applicant's name"),
            Y("sameResidentialAddress", "Do all applicants currently live at the same residential address?"),
            Y("samePhoneNumbers", "Do all applicants share the same contact telephone numbers?"),
            Q("afterHoursCountryCode", "After-hours telephone - country code"),
            Q("afterHoursAreaCode", "After-hours telephone - area code"),
            Q("afterHoursNumber", "After-hours telephone - number"),
            Q("officeHoursCountryCode", "Office-hours telephone - country code"),
            Q("officeHoursAreaCode", "Office-hours telephone - area code"),
            Q("officeHoursNumber", "Office-hours telephone - number"),
            Q("mobileCountryCode", "Mobile telephone - country code"),
            Q("mobileNumber", "Mobile telephone - number"),
            Y("sameEmailAddress", "Do all applicants share the same email address?"),
            E("emailAddress", "Email address"),
            Y("samePostalAddress", "Do all applicants share the same postal address?"),
            Q("postalAddress", "Postal address - street number/name or PO Box"),
            Q("postalAddressLine2", "Postal address - address line 2"),
            Q("postalCity", "Suburb/town/city"),
            Q("postalState", "State"),
            Q("postalPostcode", "Postcode"),
            Q("postalCountry", "Country")),
        Section(5, "Visa & travel history",
            Y("australianVisaHistory", "Have you previously applied for or held an Australian visa?"),
            Y("internationalTravel", "In the last 10 years, since turning 16, have you visited any country outside your usual country of residence?"),
            Y("futureTravel", "Do you have any proposed or booked travel to any country?"),
            L("futureAddresses", "Future address details")),
        Section(6, "Health details - all applicants",
            L("healthExaminationDetails", "Health examination details"),
            L("healthCareWorkDetails", "Health care work details"),
            L("agedCareWorkDetails", "Aged care work details"),
            Y("tuberculosis", "Has any applicant ever had or currently have tuberculosis, or had a chest X-ray showing an abnormality?"),
            Y("tuberculosisContact", "Has any applicant been in close contact at home or work with someone who has had tuberculosis?"),
            Y("healthConditions", "Does any applicant have any of the following conditions that may incur medical costs or require treatment or follow-up: blood disorder, cancer, diabetes, physical/intellectual disability, heart condition, hepatitis B/C, HIV/AIDS, kidney disease (including dialysis), liver disease, mental illness, pregnancy, respiratory illness or another condition?"),
            Y("mobilityAssistance", "Does any applicant require assistance with mobility or care because of a health condition?")),
        CharacterSection(7, "Character & immigration history - all applicants",
            ("policeClearance", "Has any applicant applied for a police clearance certificate in the last 12 months?"),
            ("immigrationDetention", "Has any applicant previously been in immigration detention, a refugee camp or a centre for refugees?"),
            ("criminalConviction", "Has any applicant been convicted of an offence in any country, including a conviction removed from official records? If in doubt, answer Yes."),
            ("pendingCharge", "Has any applicant been charged with an offence in any country that is awaiting legal action? If in doubt, answer Yes."),
            ("protectionOrder", "Has any applicant been subject to a domestic/family violence order or another order issued by a tribunal, court or similar authority for another person's protection?"),
            ("arrestWarrant", "Has any applicant been subject to an arrest warrant or Interpol notice?"),
            ("childSexualOffence", "Has any applicant been found guilty of a sexual offence involving a child, including where no conviction was recorded?"),
            ("sexOffenderRegister", "Has any applicant been named on a sex offender register?"),
            ("courtConfinement", "Has any applicant been confined in a prison or psychiatric institution by court order in connection with criminal proceedings?"),
            ("unsoundMindAcquittal", "Has any applicant been acquitted of an offence on grounds of unsoundness of mind or insanity? If in doubt, answer Yes."),
            ("unfitToPlead", "Has any applicant been found by a court not fit to plead?"),
            ("falseInformation", "Has any applicant supplied incorrect, false or misleading information or documents to Australian Immigration or Customs authorities?"),
            ("visaRefusal", "Has any applicant had a visa or entry permit refused by any country, including Australia?"),
            ("visaOverstay", "Has any applicant overstayed a visa or entry permit in any country, including Australia?"),
            ("deported", "Has any applicant been removed or deported from any country, including Australia?"),
            ("avoidedDeportation", "Has any applicant left a country to avoid removal or deportation, including Australia?"),
            ("excludedFromCountry", "Has any applicant been excluded from or asked to leave any country, including Australia?"),
            ("citizenshipRefused", "Has any applicant had citizenship refused, renounced or rescinded?"),
            ("internationalCrimes", "Has any applicant been charged with or indicted for genocide, war crimes, crimes against humanity, torture, slavery or another crime of serious international concern?"),
            ("nationalSecurityRisk", "Has any applicant been directly or indirectly involved in or associated with activities posing a national security risk to Australia or another country?"),
            ("governmentDebt", "Has any applicant had outstanding debts to the Australian Government or an Australian public authority?"),
            ("peopleSmuggling", "Has any applicant been involved in people smuggling or trafficking offences? If in doubt, answer Yes."),
            ("criminalAssociation", "Has any applicant been associated with a person, group or organisation involved in criminal conduct?"),
            ("violentOrganisation", "Has any applicant been associated with an organisation engaged in violence, or personally engaged in violence, including war, insurgency, freedom fighting, terrorism or protest, in Australia or overseas?"),
            ("militaryTraining", "Has any applicant undertaken military/paramilitary training, weapons/explosives training or training in manufacturing chemical/biological products?"),
            ("militaryService", "Has any applicant served in a military force, police force, state-sponsored militia, private militia, secret police or intelligence agency?"),
            ("sponsorshipBenefit", "Has any applicant made or offered a payment or another benefit in return for sponsorship, nomination or support for an Australian visa?")),
        Section(8, "Declaration",
            Y("declarationConfirmed", "I understand and accept the terms and conditions and confirm that the information supplied is true and correct. I authorise my agent to make all necessary enquiries to determine my eligibility, including visa entitlement checks with immigration authorities. Do you confirm this declaration?"),
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
                L(question.Key + "Details", "If yes, provide details where applicable.")
            }).ToArray());

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
