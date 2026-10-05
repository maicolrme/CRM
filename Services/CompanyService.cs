using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface ICompanyService
{
    Task<List<Company>> SearchAsync(string? search);
    Task<Company?> GetAsync(int id);
    Task<List<Contact>> GetContactsAsync(int companyId);
    Task<List<Customer>> GetCustomersAsync(int companyId);
    Task<int> CountAsync();
    Task CreateAsync(CompanyFormViewModel model);
    Task<bool> UpdateAsync(CompanyFormViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<List<Company>> AllAsync();
}

public class CompanyService : ICompanyService
{
    private readonly AppDbContext _db;

    public CompanyService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Company>> SearchAsync(string? search)
    {
        var query = _db.Companies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(term) ||
                (c.Email != null && c.Email.ToLower().Contains(term)) ||
                (c.Website != null && c.Website.ToLower().Contains(term)));
        }

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public Task<Company?> GetAsync(int id) =>
        _db.Companies.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<List<Contact>> GetContactsAsync(int companyId) =>
        await _db.Contacts.Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.FirstName).ToListAsync();

    public async Task<List<Customer>> GetCustomersAsync(int companyId) =>
        await _db.Customers.Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.FirstName).ToListAsync();

    public Task<int> CountAsync() => _db.Companies.CountAsync();

    public async Task CreateAsync(CompanyFormViewModel model)
    {
        _db.Companies.Add(new Company
        {
            Name = model.Name.Trim(),
            Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
            Website = string.IsNullOrWhiteSpace(model.Website) ? null : model.Website.Trim(),
            Address = string.IsNullOrWhiteSpace(model.Address) ? null : model.Address.Trim(),
            Notes = model.Notes
        });

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(CompanyFormViewModel model)
    {
        var company = await _db.Companies.FindAsync(model.Id);
        if (company is null) return false;

        company.Name = model.Name.Trim();
        company.Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
        company.Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim();
        company.Website = string.IsNullOrWhiteSpace(model.Website) ? null : model.Website.Trim();
        company.Address = string.IsNullOrWhiteSpace(model.Address) ? null : model.Address.Trim();
        company.Notes = model.Notes;
        company.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var company = await _db.Companies.FindAsync(id);
        if (company is null) return false;

        _db.Companies.Remove(company);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Company>> AllAsync() =>
        await _db.Companies.OrderBy(c => c.Name).ToListAsync();
}
