using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models;

public class Contact
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public Company? Company { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress, MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(150)]
    public string? Position { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
