using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data;
using MyApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);
var databaseDirectory = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(databaseDirectory);
var databasePath = Path.Combine(databaseDirectory, "myapp.db");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));
builder.Services.AddAntiforgery(options =>
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always);
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 12;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using (var scope = app.Services.CreateAsyncScope())
{
    var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await database.Database.EnsureCreatedAsync();
    await database.Database.ExecuteSqlRawAsync("""
        CREATE TABLE IF NOT EXISTS "QuestionnaireSubmissions" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_QuestionnaireSubmissions" PRIMARY KEY AUTOINCREMENT,
            "UserId" TEXT NOT NULL,
            "AnswersProtected" TEXT NOT NULL,
            "QuestionnaireVersion" INTEGER NOT NULL,
            "CreatedAtUtc" TEXT NOT NULL,
            "QuestionnaireType" TEXT NOT NULL DEFAULT 'student500',
            "IsDraft" INTEGER NOT NULL DEFAULT 0,
            CONSTRAINT "FK_QuestionnaireSubmissions_AspNetUsers_UserId"
                FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
        );
        """);
    await database.Database.OpenConnectionAsync();
    var questionnaireColumns = new HashSet<string>(StringComparer.Ordinal);
    await using (var command = database.Database.GetDbConnection().CreateCommand())
    {
        command.CommandText = "PRAGMA table_info('QuestionnaireSubmissions')";
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            questionnaireColumns.Add(reader.GetString(1));
        }
    }
    await database.Database.CloseConnectionAsync();
    if (questionnaireColumns.Contains("AnswersJson") &&
        !questionnaireColumns.Contains("AnswersProtected"))
    {
        await database.Database.ExecuteSqlRawAsync("""
            ALTER TABLE "QuestionnaireSubmissions"
            RENAME COLUMN "AnswersJson" TO "AnswersProtected";
            """);
    }
    if (!questionnaireColumns.Contains("IsDraft"))
    {
        await database.Database.ExecuteSqlRawAsync("""
            ALTER TABLE "QuestionnaireSubmissions"
            ADD COLUMN "IsDraft" INTEGER NOT NULL DEFAULT 0;
            """);
    }
    if (!questionnaireColumns.Contains("QuestionnaireType"))
    {
        await database.Database.ExecuteSqlRawAsync("""
            ALTER TABLE "QuestionnaireSubmissions"
            ADD COLUMN "QuestionnaireType" TEXT NOT NULL DEFAULT 'student500';
            """);
    }
    await database.Database.ExecuteSqlRawAsync("""
        CREATE INDEX IF NOT EXISTS "IX_QuestionnaireSubmissions_UserId_CreatedAtUtc"
        ON "QuestionnaireSubmissions" ("UserId", "CreatedAtUtc");
        """);
}

app.Run();
