using System.ComponentModel.DataAnnotations;
using CrmApp.Models;

namespace CrmApp.ViewModels;

public class CustomerFormViewModel
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

    public CustomerStatus Status { get; set; } = CustomerStatus.Lead;

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public List<Company> Companies { get; set; } = new();
}

public class CustomerDetailViewModel
{
    public Customer Customer { get; set; } = null!;
    public List<Activity> Activities { get; set; } = new();
    public List<CrmTask> Tasks { get; set; } = new();
}

public class CustomerListViewModel
{
    public List<Customer> Customers { get; set; } = new();
    public string? Search { get; set; }
    public CustomerStatus? Status { get; set; }
}
