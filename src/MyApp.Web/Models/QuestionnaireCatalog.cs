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

        definition = StudentVisa;
        return false;
    }
}
