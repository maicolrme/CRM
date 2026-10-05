using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface IActivityService
{
    Task<List<Activity>> ListAsync(int? customerId = null, ActivityType? type = null);
    Task<List<Activity>> RecentAsync(int count);
    Task<int> CountAsync();
    Task CreateAsync(ActivityFormViewModel model, int userId);
    Task<Activity?> GetAsync(int id);
    Task<bool> DeleteAsync(int id);
}

public class ActivityService : IActivityService
{
    private readonly AppDbContext _db;

    public ActivityService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Activity>> ListAsync(int? customerId = null, ActivityType? type = null)
    {
        var query = _db.Activities
            .Include(a => a.Customer)
            .Include(a => a.User)
            .AsQueryable();

        if (customerId.HasValue)
            query = query.Where(a => a.CustomerId == customerId.Value);

        if (type.HasValue)
            query = query.Where(a => a.Type == type.Value);

        return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<List<Activity>> RecentAsync(int count) =>
        await _db.Activities.Include(a => a.Customer)
            .OrderByDescending(a => a.CreatedAt)
            .Take(count)
            .ToListAsync();

    public Task<int> CountAsync() => _db.Activities.CountAsync();

    public async Task CreateAsync(ActivityFormViewModel model, int userId)
    {
        _db.Activities.Add(new Activity
        {
            CustomerId = model.CustomerId,
            UserId = userId,
            Type = model.Type,
            Title = model.Title.Trim(),
            Description = model.Description,
            DueDate = model.DueDate
        });

        await _db.SaveChangesAsync();
    }

    public Task<Activity?> GetAsync(int id) =>
        _db.Activities.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var activity = await _db.Activities.FindAsync(id);
        if (activity is null) return false;

        _db.Activities.Remove(activity);
        await _db.SaveChangesAsync();
        return true;
    }
}
