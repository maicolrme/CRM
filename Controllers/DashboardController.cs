using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ICustomerService _customers;
    private readonly ITaskService _tasks;
    private readonly IActivityService _activities;
    private readonly IStringLocalizer<SharedResource> _t;

    public DashboardController(
        ICustomerService customers,
        ITaskService tasks,
        IActivityService activities,
        IStringLocalizer<SharedResource> t)
    {
        _customers = customers;
        _tasks = tasks;
        _activities = activities;
        _t = t;
    }

    [HttpGet("/dashboard")]
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalCustomers = await _customers.CountAsync(),
            OpenTasks = await _tasks.OpenCountAsync(),
            TotalActivities = await _activities.CountAsync(),
            RecentCustomers = await _customers.RecentAsync(5),
            UpcomingTasks = await _tasks.UpcomingAsync(5),
            RecentActivities = await _activities.RecentAsync(5)
        };

        return View(model);
    }
}
