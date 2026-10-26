using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Models;

namespace MyApp.Web.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<FormSubmission> FormSubmissions => Set<FormSubmission>();
    public DbSet<QuestionnaireSubmission> QuestionnaireSubmissions => Set<QuestionnaireSubmission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<FormSubmission>()
            .HasIndex(submission => new { submission.UserId, submission.CreatedAtUtc });
        builder.Entity<QuestionnaireSubmission>()
            .HasIndex(submission => new { submission.UserId, submission.CreatedAtUtc });
        builder.Entity<QuestionnaireSubmission>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(submission => submission.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
