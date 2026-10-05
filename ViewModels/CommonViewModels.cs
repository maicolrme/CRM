using System.ComponentModel.DataAnnotations;
using CrmApp.Models;

namespace CrmApp.ViewModels;

public class DashboardViewModel
{
    public int TotalCustomers { get; set; }
    public int OpenTasks { get; set; }
    public int TotalActivities { get; set; }
    public List<Customer> RecentCustomers { get; set; } = new();
    public List<CrmTask> UpcomingTasks { get; set; } = new();
    public List<Activity> RecentActivities { get; set; } = new();
}

public class ProfileViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Language { get; set; } = "en";
}

public class ChangePasswordViewModel
{
    [Required, DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), MinLength(8)]
    public string NewPassword { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class AdminUserViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }

    public bool IsActive { get; set; } = true;
}

public class AdminUserListViewModel
{
    public List<User> Users { get; set; } = new();
    public string? Search { get; set; }
}
