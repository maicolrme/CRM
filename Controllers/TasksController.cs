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
[Route("tasks")]
public class TasksController : Controller
{
    private readonly ITaskService _tasks;
    private readonly ICustomerService _customers;
    private readonly IStringLocalizer<SharedResource> _t;

    public TasksController(
        ITaskService tasks,
        ICustomerService customers,
        IStringLocalizer<SharedResource> t)
    {
        _tasks = tasks;
        _customers = customers;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CrmTaskStatus? status)
    {
        var model = new TaskListViewModel
        {
            Tasks = await _tasks.ListAsync(status),
            Status = status,
            Customers = await _customers.AllAsync()
        };

        ViewBag.Statuses = Enum.GetValues<CrmTaskStatus>()
            .Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = _t[$"Task.Status.{x}"].Value,
                Selected = x == status
            }).ToList();

        return View(model);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create(int? customerId)
    {
        var model = new TaskFormViewModel { CustomerId = customerId };
        return View(await BuildFormAsync(model));
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        await _tasks.CreateAsync(model, User.GetUserId());
        TempData["Message"] = _t["Task.Created"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _tasks.GetAsync(id);
        if (task is null) return NotFound();

        var model = new TaskFormViewModel
        {
            Id = task.Id,
            CustomerId = task.CustomerId,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status
        };

        return View(await BuildFormAsync(model));
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskFormViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        if (!await _tasks.UpdateAsync(model))
            return NotFound();

        TempData["Message"] = _t["Task.Updated"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/complete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int id)
    {
        if (!await _tasks.CompleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Task.Updated"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _tasks.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Task.Deleted"].Value;

        return RedirectToAction(nameof(Index));
    }

    private async Task<TaskFormViewModel> BuildFormAsync(TaskFormViewModel model)
    {
        var customers = await _customers.AllAsync();
        ViewBag.Customers = new SelectList(customers, "Id", "FullName", model.CustomerId);
        ViewBag.Statuses = Enum.GetValues<CrmTaskStatus>()
            .Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = _t[$"Task.Status.{x}"].Value,
                Selected = x == model.Status
            }).ToList();

        return model;
    }
}
