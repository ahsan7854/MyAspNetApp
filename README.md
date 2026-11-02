# MyAspNetApp

This is an ASP.NET Core MVC application with authenticated, database-backed visa questionnaire submissions.

## Main folders

- `src/` - Application source code
  - `MyApp.Web/` - ASP.NET web project
  - `MyApp.Application/` - Business logic and application services
  - `MyApp.Domain/` - Core domain entities, interfaces, value objects
  - `MyApp.Infrastructure/` - Data access, repositories, external services
  - `MyApp.Shared/` - Shared utilities and common code
- `tests/` - Automated tests
  - `MyApp.UnitTests/` - Unit tests
- `docs/` - Technical documentation and project notes
- `scripts/` - Deployment/build/setup scripts

## Run the web application

Install the .NET 10 SDK, then run these commands from the repository root:

```powershell
dotnet restore src\MyApp.Web\MyApp.Web.csproj
dotnet run --project src\MyApp.Web\MyApp.Web.csproj
```

Open the local URL printed by `dotnet run`. Select **Sign in to continue**, then
create an account. Registration requires a password of at least 12 characters
with uppercase, lowercase, a number, and a symbol.

## Submission workflow

1. Register or sign in. Account passwords are managed and hashed by ASP.NET
   Core Identity. After successful authentication, the app opens the business
   particulars page.
2. Review the Anchor Migration and Education Consultants particulars and select
   **Continue to questionnaire**, or select a supported visa questionnaire from
   the **Visa types** menu.
3. Complete the sections of the selected questionnaire using the tabs. The
   Student Visa, Graduate (Subclass 485), Graduate (Subclass 485) No Form 80,
   Business Visa, ENS/RSMS, Skilled (189/190), Temporary Activity (408),
   Temporary Work (400), Skills in Demand (482) and Subsequent Entrant,
   Aged/Remaining Relative, Child Visa (Over 18 and Under 18), Parent
   (Permanent), Partner Visa, Visitor Visa, Form 80 for applicants over 16,
   and Talent (124/858) questionnaires, with and without Form 80, are available.
   Save progress at any time and continue later from **My questionnaires**.
   Add records where needed, then confirm the declaration on the final tab and
   submit the questionnaire.
4. The application validates the answers on the server and associates the
   saved draft or submitted questionnaire with the signed-in account.
5. The **My questionnaires** page lists only that user's drafts and submissions,
   with links to resume drafts or review submitted answers.

## Client questionnaire work report

The task breakdown below covers the shared questionnaire workflow and all 20
questionnaire templates currently available in the application. Time tracking
was not captured while the work was completed, so the hours are retrospective
estimates for backlog and sprint planning, not verified historical hours.

| Task | Brief description | Estimated time |
|---|---|---:|
| Shared questionnaire workflow | Build the reusable questionnaire catalog and multi-section form flow, including tab navigation, answer validation, draft saving and resuming, repeatable records, final declaration, submission, and account-linked storage. | 12 hours |
| Student Visa Questionnaire | Create the baseline student visa questionnaire template and its sections and questions. | 6 hours |
| Aged/Remaining Relative Visas Questionnaire | Add the aged and remaining relative visa questions and sections. | 4 hours |
| Child Visa Questionnaire (Over 18) | Add the over-18 child visa questionnaire template. | 4 hours |
| Child Visa Questionnaire (Under 18) | Add the under-18 child visa questionnaire template. | 4 hours |
| Parent (Permanent) Visa Questionnaire | Add the permanent parent visa questionnaire template. | 4 hours |
| Partner Visas Questionnaire | Add the partner visa questionnaire template. | 5 hours |
| Partner Visas Questionnaire - No Form 80 | Add the partner visa variant that excludes Form 80 questions. | 2 hours |
| Business Visa Client Questionnaire | Add the business visa questionnaire and its detailed sections and questions. | 8 hours |
| ENS/RSMS Visa Questionnaire | Add the employer nomination and regional sponsored visa questionnaire. | 2 hours |
| Graduate (485) Visa Questionnaire | Add the graduate visa questionnaire template. | 5 hours |
| Graduate (485) Visa Questionnaire - No Form 80 | Add the graduate visa variant that excludes Form 80 questions. | 4 hours |
| Skilled (189/190) Visas Questionnaire | Add the skilled independent and nominated visa questionnaire. | 3 hours |
| Temporary Activity (408) Visa Questionnaire | Add the temporary activity visa questionnaire. | 3 hours |
| Temporary Work (400) Visa Questionnaire | Add the temporary work visa questionnaire and its supporting questions. | 4 hours |
| Skills in Demand (482) Questionnaire | Add the Skills in Demand visa questionnaire. | 3 hours |
| Skills in Demand (482) Subsequent Entrant Questionnaire | Add the subsequent entrant questionnaire variant. | 3 hours |
| Talent (124/858) Visa Questionnaire | Add the talent visa questionnaire template. | 4 hours |
| Talent (124/858) Visa Questionnaire - No Form 80 | Add the talent visa variant that excludes Form 80 questions. | 2 hours |
| Visitor Visa Questionnaire | Add the visitor visa questionnaire template. | 4 hours |
| Form 80 Questionnaire for Over 16 Applicants | Add the Form 80 questionnaire for applicants over 16. | 4 hours |
| Cross-template verification | Check questionnaire navigation, saving/resuming, validation, repeatable answers, and submission across the available templates. | 8 hours |
| **Estimated total** | **Shared workflow, 20 templates, and cross-template verification.** | **98 hours** |

ASP.NET Core Identity and questionnaire submissions are stored in the SQLite
database at `src\MyApp.Web\App_Data\myapp.db`, in the
`QuestionnaireSubmissions` table. Each submission has a row containing its
owner, date, questionnaire version, and answers. Answers are stored as versioned
JSON protected with ASP.NET Core Data Protection, allowing repeatable
questionnaire records without a fixed database column for every answer. On
startup, the application ensures the questionnaire table and its user/date
index exist; existing form submissions and identity data are preserved. Keep
the Data Protection key ring available to the application and back it up along
with the database as part of deployment operations. The encrypted answer
column is intentionally not readable as plain text in a SQLite viewer; the
client can review their answers from **My questionnaires**.

The app uses HTTPS redirection. For local development, trust the ASP.NET Core
development certificate if prompted by the SDK.
