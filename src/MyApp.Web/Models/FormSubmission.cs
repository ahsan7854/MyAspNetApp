using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models;

public sealed class FormSubmission
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string ContactEmail { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? StreetAddress { get; set; }

    [StringLength(80)]
    public string? City { get; set; }

    [StringLength(80)]
    public string? Region { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [Required, StringLength(40)]
    public string Category { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(3000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string PreferredContactMethod { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
