using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize]
[Route("contacts")]
public class ContactsController : Controller
{
    private readonly IContactService _contacts;
    private readonly ICompanyService _companies;
    private readonly IStringLocalizer<SharedResource> _t;

    public ContactsController(
        IContactService contacts,
        ICompanyService companies,
        IStringLocalizer<SharedResource> t)
    {
        _contacts = contacts;
        _companies = companies;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? search)
    {
        return View(new ContactListViewModel
        {
            Contacts = await _contacts.SearchAsync(search),
            Search = search
        });
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create() => View(await BuildFormAsync(new ContactFormViewModel()));

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        await _contacts.CreateAsync(model);
        TempData["Message"] = _t["Contact.Created"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var contact = await _contacts.GetAsync(id);
        if (contact is null) return NotFound();

        var model = new ContactFormViewModel
        {
            Id = contact.Id,
            CompanyId = contact.CompanyId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Phone = contact.Phone,
            Position = contact.Position
        };

        return View(await BuildFormAsync(model));
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContactFormViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
            return View(await BuildFormAsync(model));

        if (!await _contacts.UpdateAsync(model))
            return NotFound();

        TempData["Message"] = _t["Contact.Updated"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _contacts.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Contact.Deleted"].Value;

        return RedirectToAction(nameof(Index));
    }

    private async Task<ContactFormViewModel> BuildFormAsync(ContactFormViewModel model)
    {
        var companies = await _companies.AllAsync();
        ViewBag.Companies = new SelectList(companies, "Id", "Name", model.CompanyId);
        return model;
    }
}
