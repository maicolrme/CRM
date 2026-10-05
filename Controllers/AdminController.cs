using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin")]
public class AdminController : Controller
{
    private readonly IAdminService _admin;
    private readonly IStringLocalizer<SharedResource> _t;

    public AdminController(IAdminService admin, IStringLocalizer<SharedResource> t)
    {
        _admin = admin;
        _t = t;
    }

    [HttpGet("")]
    public IActionResult Index() => RedirectToAction(nameof(Users));

    [HttpGet("users")]
    public async Task<IActionResult> Users(string? search)
    {
        return View(new AdminUserListViewModel
        {
            Users = await _admin.UsersAsync(search),
            Search = search
        });
    }

    [HttpGet("users/{id:int}")]
    public async Task<IActionResult> UserEdit(int id)
    {
        var user = await _admin.GetUserAsync(id);
        if (user is null) return NotFound();

        return View(new AdminUserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            IsAdmin = user.IsAdmin,
            IsActive = user.IsActive
        });
    }

    [HttpPost("users/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserEdit(int id, AdminUserViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
            return View(model);

        if (!await _admin.UpdateUserAsync(id, model))
        {
            ModelState.AddModelError(nameof(model.Email), _t["Auth.EmailExists"].Value);
            return View(model);
        }

        TempData["Message"] = _t["Admin.Created"].Value;

        return RedirectToAction(nameof(Users));
    }

    [HttpPost("users/{id:int}/toggle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        if (!await _admin.ToggleActiveAsync(id))
            return NotFound();

        TempData["Message"] = _t["Admin.Created"].Value;

        return RedirectToAction(nameof(Users));
    }

    [HttpPost("users/{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == User.GetUserId())
            return RedirectToAction(nameof(Users));

        if (!await _admin.DeleteAsync(id))
            return NotFound();

        TempData["Message"] = _t["Admin.Deleted"].Value;

        return RedirectToAction(nameof(Users));
    }

    [HttpGet("roles")]
    public IActionResult Roles() => View();
}
