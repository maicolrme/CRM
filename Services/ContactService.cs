using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface IContactService
{
    Task<List<Contact>> SearchAsync(string? search);
    Task<Contact?> GetAsync(int id);
    Task<int> CountAsync();
    Task CreateAsync(ContactFormViewModel model);
    Task<bool> UpdateAsync(ContactFormViewModel model);
    Task<bool> DeleteAsync(int id);
}

public class ContactService : IContactService
{
    private readonly AppDbContext _db;

    public ContactService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Contact>> SearchAsync(string? search)
    {
        var query = _db.Contacts.Include(c => c.Company).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(term) ||
                c.LastName.ToLower().Contains(term) ||
                (c.Email != null && c.Email.ToLower().Contains(term)) ||
                (c.Position != null && c.Position.ToLower().Contains(term)) ||
                (c.Company != null && c.Company.Name.ToLower().Contains(term)));
        }

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public Task<Contact?> GetAsync(int id) =>
        _db.Contacts.Include(c => c.Company).FirstOrDefaultAsync(c => c.Id == id);

    public Task<int> CountAsync() => _db.Contacts.CountAsync();

    public async Task CreateAsync(ContactFormViewModel model)
    {
        _db.Contacts.Add(new Contact
        {
            CompanyId = model.CompanyId,
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
            Position = string.IsNullOrWhiteSpace(model.Position) ? null : model.Position.Trim()
        });

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(ContactFormViewModel model)
    {
        var contact = await _db.Contacts.FindAsync(model.Id);
        if (contact is null) return false;

        contact.CompanyId = model.CompanyId;
        contact.FirstName = model.FirstName.Trim();
        contact.LastName = model.LastName.Trim();
        contact.Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
        contact.Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim();
        contact.Position = string.IsNullOrWhiteSpace(model.Position) ? null : model.Position.Trim();
        contact.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _db.Contacts.FindAsync(id);
        if (contact is null) return false;

        _db.Contacts.Remove(contact);
        await _db.SaveChangesAsync();
        return true;
    }
}
