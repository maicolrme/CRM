using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models;

public class Customer
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress, MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    public int? CompanyId { get; set; }

    public Company? Company { get; set; }

    public CustomerStatus Status { get; set; } = CustomerStatus.Lead;

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public ICollection<CrmTask> Tasks { get; set; } = new List<CrmTask>();

    public string FullName => $"{FirstName} {LastName}";
}
