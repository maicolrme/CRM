using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User?> RegisterAsync(RegisterViewModel model);
    Task<User?> GetByIdAsync(int id);
    Task UpdateProfileAsync(ProfileViewModel model);
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    Task UpdateLanguageAsync(int userId, string language);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());

        if (user is null || !user.IsActive || !PasswordHasher.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<User?> RegisterAsync(RegisterViewModel model)
    {
        var email = model.Email.ToLower().Trim();

        if (await _db.Users.AnyAsync(u => u.Email == email))
            return null;

        var user = new User
        {
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Email = email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            IsAdmin = false
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return user;
    }

    public Task<User?> GetByIdAsync(int id) =>
        _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task UpdateProfileAsync(ProfileViewModel model)
    {
        var user = await _db.Users.FindAsync(model.Id);
        if (user is null) return;

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        user.Email = model.Email.ToLower().Trim();
        user.Language = model.Language;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user is null || !PasswordHasher.Verify(currentPassword, user.PasswordHash))
            return false;

        user.PasswordHash = PasswordHasher.Hash(newPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task UpdateLanguageAsync(int userId, string language)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user is null) return;

        user.Language = language;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }
}
