using System.Security.Claims;
using CrmApp.Resources;
using CrmApp.Services;
using CrmApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CrmApp.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _auth;
    private readonly IStringLocalizer<SharedResource> _t;

    public AccountController(IAuthService auth, IStringLocalizer<SharedResource> t)
    {
        _auth = auth;
        _t = t;
    }

    [HttpGet("/login")]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost("/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _auth.AuthenticateAsync(model.Email, model.Password);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _t["Auth.InvalidCredentials"].Value);
            return View(model);
        }

        await SignInAsync(user.Id, user.IsAdmin, user.FullName);

        await _auth.UpdateLanguageAsync(user.Id, System.Globalization.CultureInfo.CurrentUICulture.Name);

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            return Redirect(model.ReturnUrl);

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet("/register")]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");

        return View(new RegisterViewModel());
    }

    [HttpPost("/register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _auth.RegisterAsync(model);

        if (user is null)
        {
            ModelState.AddModelError(nameof(model.Email), _t["Auth.EmailExists"].Value);
            return View(model);
        }

        await SignInAsync(user.Id, user.IsAdmin, user.FullName);

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost("/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return Redirect("/login");
    }

    private async Task SignInAsync(int id, bool isAdmin, string name)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Name, name),
            new(ClaimTypes.Role, isAdmin ? "Admin" : "User"),
            new("IsAdmin", isAdmin ? "true" : "false")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("Cookies", principal,
            new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) });
    }
}
