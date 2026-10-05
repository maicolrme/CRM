using CrmApp.Models;
using CrmApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();

        if (await db.Users.AnyAsync())
            return;

        var admin = new User
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@example.com",
            PasswordHash = PasswordHasher.Hash("Admin123!"),
            IsAdmin = true,
            Language = "en"
        };

        var user = new User
        {
            FirstName = "Regular",
            LastName = "User",
            Email = "user@example.com",
            PasswordHash = PasswordHasher.Hash("User123!"),
            IsAdmin = false,
            Language = "en"
        };

        db.Users.AddRange(admin, user);

        var companies = new[]
        {
            new Company { Name = "Acme S.r.l.", Email = "info@acme.it", Phone = "+39 02 1234567", Website = "https://acme.it", Address = "Via Roma 1, Milano" },
            new Company { Name = "Tech Solutions", Email = "hello@techsolutions.com", Phone = "+1 415 555 0132", Website = "https://techsolutions.com", Address = "500 Howard St, San Francisco" },
            new Company { Name = "Global Trading Ltd", Email = "contact@globaltrading.co.uk", Phone = "+44 20 7946 0958", Website = "https://globaltrading.co.uk", Address = "12 Bishopsgate, London" }
        };
        db.Companies.AddRange(companies);

        var customers = new[]
        {
            new Customer { FirstName = "John", LastName = "Smith", Email = "john.smith@acme.it", Phone = "+39 02 7654321", CompanyId = 1, Status = CustomerStatus.Customer, Notes = "Key account, prefers email contact." },
            new Customer { FirstName = "Maria", LastName = "Rossi", Email = "maria.rossi@techsolutions.com", Phone = "+1 415 555 0199", CompanyId = 2, Status = CustomerStatus.Prospect, Notes = "Interested in the enterprise plan." },
            new Customer { FirstName = "Luca", LastName = "Bianchi", Email = "luca.bianchi@example.com", Phone = "+39 333 1234567", Status = CustomerStatus.Lead },
            new Customer { FirstName = "Emma", LastName = "Wilson", Email = "emma.wilson@globaltrading.co.uk", Phone = "+44 20 7946 0999", CompanyId = 3, Status = CustomerStatus.Customer },
            new Customer { FirstName = "Carlos", LastName = "Garcia", Email = "carlos.garcia@example.es", Phone = "+34 91 1234567", Status = CustomerStatus.Inactive }
        };
        db.Customers.AddRange(customers);

        var contacts = new[]
        {
            new Contact { CompanyId = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@acme.it", Phone = "+39 02 7654321", Position = "CEO" },
            new Contact { CompanyId = 1, FirstName = "Sara", LastName = "Conti", Email = "sara.conti@acme.it", Position = "Procurement Manager" },
            new Contact { CompanyId = 2, FirstName = "Maria", LastName = "Rossi", Email = "maria.rossi@techsolutions.com", Position = "CTO" },
            new Contact { CompanyId = 3, FirstName = "Emma", LastName = "Wilson", Email = "emma.wilson@globaltrading.co.uk", Position = "Head of Sales" }
        };
        db.Contacts.AddRange(contacts);

        await db.SaveChangesAsync();

        var now = DateTime.UtcNow;

        db.Activities.AddRange(
            new Activity { CustomerId = 1, UserId = admin.Id, Type = ActivityType.Call, Title = "Discovery call", Description = "Discussed current pipeline and next steps.", DueDate = now.AddDays(-5) },
            new Activity { CustomerId = 2, UserId = admin.Id, Type = ActivityType.Meeting, Title = "Product demo", Description = "Demo of the CRM modules.", DueDate = now.AddDays(-2) },
            new Activity { CustomerId = 3, UserId = user.Id, Type = ActivityType.Email, Title = "Sent pricing", Description = "Shared pricing table and FAQ.", DueDate = now.AddDays(-1) },
            new Activity { CustomerId = 4, UserId = admin.Id, Type = ActivityType.Note, Title = "Renewal discussion", Description = "Contract renews next quarter." }
        );

        db.Tasks.AddRange(
            new CrmTask { CustomerId = 1, UserId = admin.Id, Title = "Call Acme", Description = "Follow up on the demo feedback.", DueDate = now.AddDays(1), Status = CrmTaskStatus.Pending },
            new CrmTask { CustomerId = 3, UserId = user.Id, Title = "Follow up with John", DueDate = now.AddDays(2), Status = CrmTaskStatus.Pending },
            new CrmTask { CustomerId = 2, UserId = admin.Id, Title = "Send proposal", Description = "Prepare the enterprise proposal.", DueDate = now.AddDays(3), Status = CrmTaskStatus.InProgress },
            new CrmTask { CustomerId = 4, UserId = admin.Id, Title = "Prepare renewal contract", DueDate = now.AddDays(10), Status = CrmTaskStatus.Pending },
            new CrmTask { UserId = user.Id, Title = "Update CRM records", DueDate = now.AddDays(-1), Status = CrmTaskStatus.Completed }
        );

        await db.SaveChangesAsync();
    }
}
