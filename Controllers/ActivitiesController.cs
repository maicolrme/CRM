using CrmApp.Models;
using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize]
[Route("activities")]
public class ActivitiesController : Controller
{
    private readonly IActivityService _activities;
    private readonly ICustomerService _customers;
    private readonly IStringLocalizer<SharedResource> _t;

    public ActivitiesController(
        IActivityService activities,
        ICustomerService customers,
        IStringLocalizer<SharedResource> t)
    {
        _activities = activities;
        _customers = customers;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(int? customerId, ActivityType? type)
    {
        var model = new ActivityListViewModel
        {
            Activities = await _activities.ListAsync(customerId, type),
            CustomerId = customerId,
            Type = type,
            Customers = await _customers.AllAsync()
        };

        ViewBag.Customers = new SelectList(model.Customers, "Id", "FullName", customerId);
        ViewBag.Types = Enum.GetValues<ActivityType>()
            .Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = _t[$"Activity.Type.{x}"].Value,
                Selected = x == type
            }).ToList();

        return View(model);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create(int? customerId)
    {
        var model = new ActivityFormViewModel { CustomerId = customerId ?? 0 };
        return View(await BuildFormAsync(model));
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ActivityFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        await _activities.CreateAsync(model, User.GetUserId());
        TempData["Message"] = _t["Activity.Created"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _activities.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Activity.Deleted"].Value;

        return RedirectToAction(nameof(Index));
    }

    private async Task<ActivityFormViewModel> BuildFormAsync(ActivityFormViewModel model)
    {
        var customers = await _customers.AllAsync();
        ViewBag.Customers = new SelectList(customers, "Id", "FullName", model.CustomerId);
        ViewBag.Types = Enum.GetValues<ActivityType>()
            .Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = _t[$"Activity.Type.{x}"].Value,
                Selected = x == model.Type
            }).ToList();

        return model;
    }
}
