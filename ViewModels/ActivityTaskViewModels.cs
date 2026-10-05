using System.ComponentModel.DataAnnotations;
using CrmApp.Models;

namespace CrmApp.ViewModels;

public class ActivityFormViewModel
{
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public ActivityType Type { get; set; } = ActivityType.Note;

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DueDate { get; set; }

    public List<Customer> Customers { get; set; } = new();
}

public class ActivityListViewModel
{
    public List<Activity> Activities { get; set; } = new();
    public int? CustomerId { get; set; }
    public ActivityType? Type { get; set; }
    public List<Customer> Customers { get; set; } = new();
}

public class TaskFormViewModel
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DueDate { get; set; }

    public CrmTaskStatus Status { get; set; } = CrmTaskStatus.Pending;

    public List<Customer> Customers { get; set; } = new();
}

public class TaskListViewModel
{
    public List<CrmTask> Tasks { get; set; } = new();
    public CrmTaskStatus? Status { get; set; }
    public int? CustomerId { get; set; }
    public List<Customer> Customers { get; set; } = new();
}
