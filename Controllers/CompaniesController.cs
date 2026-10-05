using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize]
[Route("companies")]
public class CompaniesController : Controller
{
    private readonly ICompanyService _companies;
    private readonly IStringLocalizer<SharedResource> _t;

    public CompaniesController(ICompanyService companies, IStringLocalizer<SharedResource> t)
    {
        _companies = companies;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? search)
    {
        return View(new CompanyListViewModel
        {
            Companies = await _companies.SearchAsync(search),
            Search = search
        });
    }

    [HttpGet("create")]
    public IActionResult Create() => View(new CompanyFormViewModel());

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompanyFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _companies.CreateAsync(model);
        TempData["Message"] = _t["Company.Created"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var company = await _companies.GetAsync(id);
        if (company is null) return NotFound();

        ViewBag.Contacts = await _companies.GetContactsAsync(id);
        ViewBag.Customers = await _companies.GetCustomersAsync(id);

        return View(company);
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var company = await _companies.GetAsync(id);
        if (company is null) return NotFound();

        return View(new CompanyFormViewModel
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email,
            Phone = company.Phone,
            Website = company.Website,
            Address = company.Address,
            Notes = company.Notes
        });
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompanyFormViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
            return View(model);

        if (!await _companies.UpdateAsync(model))
            return NotFound();

        TempData["Message"] = _t["Company.Updated"].Value;

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _companies.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Company.Deleted"].Value;

        return RedirectToAction(nameof(Index));
    }
}
