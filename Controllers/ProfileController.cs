using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

[Authorize]
[Route("profile")]
public class ProfileController : Controller
{
    private readonly IAuthService _auth;
    private readonly IStringLocalizer<SharedResource> _t;

    public ProfileController(IAuthService auth, IStringLocalizer<SharedResource> t)
    {
        _auth = auth;
        _t = t;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var user = await _auth.GetByIdAsync(User.GetUserId());
        if (user is null) return Unauthorized();

        return View(new ProfileViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Language = user.Language
        });
    }

    [HttpPost("")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _auth.UpdateProfileAsync(model);
        TempData["Message"] = _t["Profile.Saved"].Value;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("password")]
    public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

    [HttpPost("password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (!await _auth.ChangePasswordAsync(User.GetUserId(), model.CurrentPassword, model.NewPassword))
        {
            ModelState.AddModelError(nameof(model.CurrentPassword), _t["Profile.WrongPassword"].Value);
            return View(model);
        }

        TempData["Message"] = _t["Profile.PasswordChanged"].Value;

        return RedirectToAction(nameof(Index));
    }
}
