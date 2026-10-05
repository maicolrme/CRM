using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface IAdminService
{
    Task<List<User>> UsersAsync(string? search);
    Task<User?> GetUserAsync(int id);
    Task<bool> UpdateUserAsync(int id, AdminUserViewModel model);
    Task<bool> ToggleActiveAsync(int id);
    Task<bool> DeleteAsync(int id);
}

public class AdminService : IAdminService
{
    private readonly AppDbContext _db;

    public AdminService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> UsersAsync(string? search)
    {
        var query = _db.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(u =>
                u.FirstName.ToLower().Contains(term) ||
                u.LastName.ToLower().Contains(term) ||
                u.Email.ToLower().Contains(term));
        }

        return await query.OrderBy(u => u.FirstName).ToListAsync();
    }

    public Task<User?> GetUserAsync(int id) =>
        _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<bool> UpdateUserAsync(int id, AdminUserViewModel model)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;

        var email = model.Email.ToLower().Trim();

        if (await _db.Users.AnyAsync(u => u.Email == email && u.Id != id))
            return false;

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        user.Email = email;
        user.IsAdmin = model.IsAdmin;
        user.IsActive = model.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }
}
