using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models;

public class CrmTask
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public CrmTaskStatus Status { get; set; } = CrmTaskStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
