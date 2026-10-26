# MyAspNetApp

This is an ASP.NET Core MVC application with authenticated, database-backed student visa questionnaire submissions.

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

1. Open the form link. The form endpoints require an authenticated account and
   redirect signed-out visitors to sign in.
2. Register or sign in. Account passwords are managed and hashed by ASP.NET
   Core Identity.
3. Complete the 16 sections of the Student Visa Questionnaire, add records
   where needed, and confirm the declaration.
4. The application validates the answers on the server and associates the
   saved questionnaire with the signed-in account.
5. The **My questionnaires** page lists only that user's submissions and links
   to the saved answers.

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
