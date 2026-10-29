namespace MyApp.Web.Models;

public sealed record QuestionnaireDefinition(
    string Type,
    string Title,
    int Version,
    IReadOnlyList<string> TabTitles,
    IReadOnlyList<FormQuestionnaireSection> Sections);

public static class QuestionnaireCatalog
{
    public const string StudentVisaType = "student500";
    public const string AgedRemainingRelativeType = "agedRemainingRelative";
    public const string ChildOver18Type = "childOver18";
    public const string ChildUnder18Type = "childUnder18";
    public const string ParentPermanentType = "parentPermanent";
    public const string PartnerVisaType = "partnerVisa";
    public const string PartnerVisaNoForm80Type = "partnerVisaNoForm80";
    public const string BusinessVisaType = "businessVisa";
    public const string EnsRsmsType = "ensRsms";
    public const string Graduate485NoForm80Type = "graduate485NoForm80";
    public const string Skilled189190Type = "skilled189190";
    public const string TemporaryActivity408Type = "temporaryActivity408";
    public const string TemporaryWork400Type = "temporaryWork400";
    public const string SkillsInDemand482Type = "skillsInDemand482";
    public const string SkillsInDemand482SubsequentEntrantType = "skillsInDemand482SubsequentEntrant";
    public const string Talent124858Type = "talent124858";
    public const string Talent124858NoForm80Type = "talent124858NoForm80";
    public const string VisitorVisaType = "visitorVisa";
    public const string Form80Over16Type = "form80Over16";

    public static QuestionnaireDefinition StudentVisa { get; } = new(
        StudentVisaType,
        "Student Visa Questionnaire",
        FormQuestionnaire.CurrentVersion,
        [
            "Applicant details",
            "Citizenship & passport",
            "Identity & employment",
            "Education & languages",
            "Spouse/partner details",
            "Spouse/partner identity",
            "Children",
            "Other family",
            "Addresses & phones",
            "Email & travel",
            "Study & funding",
            "Health & insurance",
            "Police & character",
            "Immigration history",
            "Security & sponsorship",
            "Declaration"
        ],
        FormQuestionnaire.Sections);

    public static QuestionnaireDefinition Graduate485 { get; } = new(
        Graduate485Questionnaire.Type,
        Graduate485Questionnaire.Title,
        Graduate485Questionnaire.CurrentVersion,
        [
            "Applicant details",
            "Citizenship & identity",
            "Contact details",
            "Employment, education & family",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Declaration"
        ],
        Graduate485Questionnaire.Sections);

    public static QuestionnaireDefinition Graduate485NoForm80 { get; } = new(
        Graduate485NoForm80Questionnaire.Type,
        Graduate485NoForm80Questionnaire.Title,
        Graduate485NoForm80Questionnaire.CurrentVersion,
        [
            "Applicant details",
            "Citizenship & identity",
            "Contact details",
            "Employment, education & family",
            "Visa history",
            "Health details",
            "Character & immigration history",
            "Declaration"
        ],
        Graduate485NoForm80Questionnaire.Sections);

    public static QuestionnaireDefinition AgedRemainingRelative { get; } = new(
        AgedRemainingRelativeQuestionnaire.Type,
        AgedRemainingRelativeQuestionnaire.Title,
        AgedRemainingRelativeQuestionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Employment, education & family",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Declaration"
        ],
        AgedRemainingRelativeQuestionnaire.Sections);

    public static QuestionnaireDefinition ChildOver18 { get; } = new(
        ChildOver18Questionnaire.Type,
        ChildOver18Questionnaire.Title,
        ChildOver18Questionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Employment, education & family",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Personal contacts",
            "Declaration"
        ],
        ChildOver18Questionnaire.Sections);

    public static QuestionnaireDefinition ChildUnder18 { get; } = new(
        ChildUnder18Questionnaire.Type,
        ChildUnder18Questionnaire.Title,
        ChildUnder18Questionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Language & children",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Declaration"
        ],
        ChildUnder18Questionnaire.Sections);

    public static QuestionnaireDefinition ParentPermanent { get; } = new(
        ParentPermanentQuestionnaire.Type,
        ParentPermanentQuestionnaire.Title,
        ParentPermanentQuestionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Employment, education & family",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Personal contacts",
            "Declaration"
        ],
        ParentPermanentQuestionnaire.Sections);

    public static QuestionnaireDefinition PartnerVisa { get; } = new(
        PartnerQuestionnaire.Type,
        PartnerQuestionnaire.Title,
        PartnerQuestionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Children & relationships",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Personal contacts",
            "Declaration"
        ],
        PartnerQuestionnaire.Sections);

    public static QuestionnaireDefinition PartnerVisaNoForm80 { get; } = new(
        PartnerNoForm80Questionnaire.Type,
        PartnerNoForm80Questionnaire.Title,
        PartnerNoForm80Questionnaire.CurrentVersion,
        [
            "Personal details",
            "Citizenship & identity",
            "Education & language",
            "Children & relationships",
            "Contact details",
            "Visa & travel history",
            "Health details",
            "Character & immigration history",
            "Personal contacts",
            "Declaration"
        ],
        PartnerNoForm80Questionnaire.Sections);

    public static QuestionnaireDefinition BusinessVisa { get; } = new(
        BusinessVisaQuestionnaire.Type,
        BusinessVisaQuestionnaire.Title,
        BusinessVisaQuestionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Business",
            "Assets",
            "Intentions",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Family in Australia",
            "Declaration"
        ],
        BusinessVisaQuestionnaire.Sections);

    public static QuestionnaireDefinition EnsRsmsVisa { get; } = new(
        EnsRsmsQuestionnaire.Type,
        EnsRsmsQuestionnaire.Title,
        EnsRsmsQuestionnaire.CurrentVersion,
        [
            "Preparer",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Business",
            "Assets",
            "Intentions",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Family in Australia",
            "Declaration"
        ],
        EnsRsmsQuestionnaire.Sections);

    public static QuestionnaireDefinition Skilled189190 { get; } = new(
        Skilled189190Questionnaire.Type,
        Skilled189190Questionnaire.Title,
        Skilled189190Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Family in Australia",
            "Declaration"
        ],
        Skilled189190Questionnaire.Sections);

    public static QuestionnaireDefinition TemporaryActivity408 { get; } = new(
        TemporaryActivity408Questionnaire.Type,
        TemporaryActivity408Questionnaire.Title,
        TemporaryActivity408Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Family in Australia",
            "Declaration"
        ],
        TemporaryActivity408Questionnaire.Sections);

    public static QuestionnaireDefinition TemporaryWork400 { get; } = new(
        TemporaryWork400Questionnaire.Type,
        TemporaryWork400Questionnaire.Title,
        TemporaryWork400Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Employment",
            "Education & skills",
            "Registrations",
            "Children",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email & study",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Study, events & funds",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Declaration"
        ],
        TemporaryWork400Questionnaire.Sections);

    public static QuestionnaireDefinition SkillsInDemand482 { get; } = new(
        SkillsInDemand482Questionnaire.Type,
        SkillsInDemand482Questionnaire.Title,
        SkillsInDemand482Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports & identity",
            "Employment",
            "Education",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Declaration"
        ],
        SkillsInDemand482Questionnaire.Sections);

    public static QuestionnaireDefinition SkillsInDemand482SubsequentEntrant { get; } = new(
        SkillsInDemand482SubsequentEntrantQuestionnaire.Type,
        SkillsInDemand482SubsequentEntrantQuestionnaire.Title,
        SkillsInDemand482SubsequentEntrantQuestionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal details",
            "Children",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Declaration"
        ],
        SkillsInDemand482SubsequentEntrantQuestionnaire.Sections);

    public static QuestionnaireDefinition Talent124858 { get; } = new(
        Talent124858Questionnaire.Type,
        Talent124858Questionnaire.Title,
        Talent124858Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education & talent",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Employment offer",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Family in Australia",
            "Declaration"
        ],
        Talent124858Questionnaire.Sections);

    public static QuestionnaireDefinition Talent124858NoForm80 { get; } = new(
        Talent124858NoForm80Questionnaire.Type,
        Talent124858NoForm80Questionnaire.Title,
        Talent124858NoForm80Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal",
            "Citizenship",
            "Passports",
            "Residency",
            "Employment",
            "Education & talent",
            "Registrations",
            "Skills",
            "Languages",
            "Children",
            "Employment offer",
            "Addresses",
            "Phones",
            "Email",
            "Postal",
            "Visa history",
            "Travel history",
            "Health exams",
            "Health care",
            "Aged care",
            "Tuberculosis",
            "Conditions",
            "Police",
            "Detention",
            "Declarations",
            "Declaration"
        ],
        Talent124858NoForm80Questionnaire.Sections);

    public static QuestionnaireDefinition VisitorVisa { get; } = new(
        VisitorVisaQuestionnaire.Type,
        VisitorVisaQuestionnaire.Title,
        VisitorVisaQuestionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal details",
            "Citizenship",
            "Passports and identity",
            "Employment",
            "Children",
            "Addresses",
            "Phones",
            "Email",
            "Postal address",
            "Visa history",
            "Travel history",
            "Future travel",
            "Study, events and funds",
            "Health examinations",
            "Hospital and health care",
            "Health care work",
            "Aged care work",
            "Childcare",
            "Classroom",
            "Tuberculosis",
            "Health conditions and insurance",
            "Police clearances",
            "Detention",
            "Character declarations",
            "Personal contacts",
            "Declaration"
        ],
        VisitorVisaQuestionnaire.Sections);

    public static QuestionnaireDefinition Form80Over16 { get; } = new(
        Form80Over16Questionnaire.Type,
        Form80Over16Questionnaire.Title,
        Form80Over16Questionnaire.CurrentVersion,
        [
            "Preparer",
            "Personal details",
            "Citizenship",
            "Passports and identity",
            "Permanent residency",
            "Employment",
            "Education",
            "Children",
            "Employment and sponsorship",
            "Addresses",
            "Phones",
            "Email",
            "Postal address",
            "Visa history",
            "Travel history",
            "Future travel",
            "Future address",
            "Police clearances",
            "Detention",
            "Character declarations",
            "Personal contacts",
            "Declaration"
        ],
        Form80Over16Questionnaire.Sections);

    public static bool TryGet(string? type, out QuestionnaireDefinition definition)
    {
        if (type == StudentVisaType)
        {
            definition = StudentVisa;
            return true;
        }

        if (type == Graduate485Questionnaire.Type)
        {
            definition = Graduate485;
            return true;
        }

        if (type == Graduate485NoForm80Type)
        {
            definition = Graduate485NoForm80;
            return true;
        }

        if (type == AgedRemainingRelativeType)
        {
            definition = AgedRemainingRelative;
            return true;
        }

        if (type == ChildOver18Type)
        {
            definition = ChildOver18;
            return true;
        }

        if (type == ChildUnder18Type)
        {
            definition = ChildUnder18;
            return true;
        }

        if (type == ParentPermanentType)
        {
            definition = ParentPermanent;
            return true;
        }

        if (type == PartnerVisaType)
        {
            definition = PartnerVisa;
            return true;
        }

        if (type == PartnerVisaNoForm80Type)
        {
            definition = PartnerVisaNoForm80;
            return true;
        }

        if (type == BusinessVisaType)
        {
            definition = BusinessVisa;
            return true;
        }

        if (type == EnsRsmsType)
        {
            definition = EnsRsmsVisa;
            return true;
        }

        if (type == Skilled189190Type)
        {
            definition = Skilled189190;
            return true;
        }

        if (type == TemporaryActivity408Type)
        {
            definition = TemporaryActivity408;
            return true;
        }

        if (type == TemporaryWork400Type)
        {
            definition = TemporaryWork400;
            return true;
        }

        if (type == SkillsInDemand482Type)
        {
            definition = SkillsInDemand482;
            return true;
        }

        if (type == SkillsInDemand482SubsequentEntrantType)
        {
            definition = SkillsInDemand482SubsequentEntrant;
            return true;
        }

        if (type == Talent124858Type)
        {
            definition = Talent124858;
            return true;
        }

        if (type == Talent124858NoForm80Type)
        {
            definition = Talent124858NoForm80;
            return true;
        }

        if (type == VisitorVisaType)
        {
            definition = VisitorVisa;
            return true;
        }

        if (type == Form80Over16Type)
        {
            definition = Form80Over16;
            return true;
        }

        definition = StudentVisa;
        return false;
    }
}
