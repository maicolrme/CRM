using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface ICustomerService
{
    Task<List<Customer>> SearchAsync(string? search, CustomerStatus? status);
    Task<Customer?> GetAsync(int id);
    Task<List<Customer>> RecentAsync(int count);
    Task<int> CountAsync();
    Task CreateAsync(CustomerFormViewModel model);
    Task<bool> UpdateAsync(CustomerFormViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<List<Customer>> AllAsync();
}

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Customer>> SearchAsync(string? search, CustomerStatus? status)
    {
        var query = _db.Customers
            .Include(c => c.Company)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(c => c.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(term) ||
                c.LastName.ToLower().Contains(term) ||
                (c.Email != null && c.Email.ToLower().Contains(term)) ||
                (c.Phone != null && c.Phone.Contains(term)) ||
                (c.Company != null && c.Company.Name.ToLower().Contains(term)));
        }

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public Task<Customer?> GetAsync(int id) =>
        _db.Customers.Include(c => c.Company).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<List<Customer>> RecentAsync(int count) =>
        await _db.Customers.Include(c => c.Company)
            .OrderByDescending(c => c.CreatedAt)
            .Take(count)
            .ToListAsync();

    public Task<int> CountAsync() => _db.Customers.CountAsync();

    public async Task CreateAsync(CustomerFormViewModel model)
    {
        _db.Customers.Add(new Customer
        {
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
            CompanyId = model.CompanyId,
            Status = model.Status,
            Notes = model.Notes
        });

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(CustomerFormViewModel model)
    {
        var customer = await _db.Customers.FindAsync(model.Id);
        if (customer is null) return false;

        customer.FirstName = model.FirstName.Trim();
        customer.LastName = model.LastName.Trim();
        customer.Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
        customer.Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim();
        customer.CompanyId = model.CompanyId;
        customer.Status = model.Status;
        customer.Notes = model.Notes;
        customer.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer is null) return false;

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Customer>> AllAsync() =>
        await _db.Customers.OrderBy(c => c.FirstName).ToListAsync();
}
