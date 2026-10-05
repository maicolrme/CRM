using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models;

public class Activity
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ActivityType Type { get; set; } = ActivityType.Note;

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
