using CrmApp.Data;
using CrmApp.Models;
using CrmApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Services;

public interface ITaskService
{
    Task<List<CrmTask>> ListAsync(CrmTaskStatus? status = null, int? customerId = null);
    Task<List<CrmTask>> UpcomingAsync(int count);
    Task<int> OpenCountAsync();
    Task CreateAsync(TaskFormViewModel model, int userId);
    Task<bool> UpdateAsync(TaskFormViewModel model);
    Task<CrmTask?> GetAsync(int id);
    Task<bool> CompleteAsync(int id);
    Task<bool> DeleteAsync(int id);
}

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CrmTask>> ListAsync(CrmTaskStatus? status = null, int? customerId = null)
    {
        var query = _db.Tasks
            .Include(t => t.Customer)
            .Include(t => t.User)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (customerId.HasValue)
            query = query.Where(t => t.CustomerId == customerId.Value);

        return await query.OrderBy(t => t.DueDate ?? DateTime.MaxValue).ToListAsync();
    }

    public async Task<List<CrmTask>> UpcomingAsync(int count) =>
        await _db.Tasks.Include(t => t.Customer)
            .Where(t => t.Status != CrmTaskStatus.Completed)
            .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
            .Take(count)
            .ToListAsync();

    public async Task<int> OpenCountAsync() =>
        await _db.Tasks.CountAsync(t => t.Status != CrmTaskStatus.Completed);

    public async Task CreateAsync(TaskFormViewModel model, int userId)
    {
        _db.Tasks.Add(new CrmTask
        {
            CustomerId = model.CustomerId,
            UserId = userId,
            Title = model.Title.Trim(),
            Description = model.Description,
            DueDate = model.DueDate,
            Status = model.Status
        });

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(TaskFormViewModel model)
    {
        var task = await _db.Tasks.FindAsync(model.Id);
        if (task is null) return false;

        task.CustomerId = model.CustomerId;
        task.Title = model.Title.Trim();
        task.Description = model.Description;
        task.DueDate = model.DueDate;
        task.Status = model.Status;

        await _db.SaveChangesAsync();
        return true;
    }

    public Task<CrmTask?> GetAsync(int id) =>
        _db.Tasks.Include(t => t.Customer).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<bool> CompleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return false;

        task.Status = CrmTaskStatus.Completed;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }
}
