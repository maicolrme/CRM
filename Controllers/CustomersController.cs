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
[Route("customers")]
public class CustomersController : Controller
{
    private readonly ICustomerService _customers;
    private readonly ICompanyService _companies;
    private readonly IActivityService _activities;
    private readonly ITaskService _tasks;
    private readonly IStringLocalizer<SharedResource> _t;

    public CustomersController(
        ICustomerService customers,
        ICompanyService companies,
        IActivityService activities,
        ITaskService tasks,
        IStringLocalizer<SharedResource> t)
    {
        _customers = customers;
        _companies = companies;
        _activities = activities;
        _tasks = tasks;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? search, CustomerStatus? status)
    {
        var model = new CustomerListViewModel
        {
            Customers = await _customers.SearchAsync(search, status),
            Search = search,
            Status = status
        };

        return View(model);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        return View(await BuildFormAsync(new CustomerFormViewModel()));
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        await _customers.CreateAsync(model);
        TempData["Message"] = _t["Customer.Created"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var customer = await _customers.GetAsync(id);
        if (customer is null) return NotFound();

        var model = new CustomerDetailViewModel
        {
            Customer = customer,
            Activities = await _activities.ListAsync(id),
            Tasks = await _tasks.ListAsync(customerId: id)
        };

        return View(model);
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _customers.GetAsync(id);
        if (customer is null) return NotFound();

        var model = new CustomerFormViewModel
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            CompanyId = customer.CompanyId,
            Status = customer.Status,
            Notes = customer.Notes
        };

        return View(await BuildFormAsync(model));
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CustomerFormViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        if (!await _customers.UpdateAsync(model))
            return NotFound();

        TempData["Message"] = _t["Customer.Updated"].Value;

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _customers.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Customer.Deleted"].Value;

        return RedirectToAction(nameof(Index));
    }

    private async Task<CustomerFormViewModel> BuildFormAsync(CustomerFormViewModel model)
    {
        model.Companies = await _companies.AllAsync();
        ViewBag.Companies = new SelectList(model.Companies, "Id", "Name", model.CompanyId);
        ViewBag.Statuses = Enum.GetValues<CustomerStatus>()
            .Select(s => new SelectListItem
            {
                Value = ((int)s).ToString(),
                Text = _t[$"Customer.Status.{s}"].Value,
                Selected = s == model.Status
            }).ToList();

        return model;
    }
}
