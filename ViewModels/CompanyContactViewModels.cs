using System.ComponentModel.DataAnnotations;

namespace CrmApp.ViewModels;

public class CompanyFormViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [EmailAddress, MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(255)]
    public string? Website { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }
}

public class CompanyListViewModel
{
    public List<Models.Company> Companies { get; set; } = new();
    public string? Search { get; set; }
}

public class ContactFormViewModel
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

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

    public List<Models.Company> Companies { get; set; } = new();
}

public class ContactListViewModel
{
    public List<Models.Contact> Contacts { get; set; } = new();
    public string? Search { get; set; }
}
