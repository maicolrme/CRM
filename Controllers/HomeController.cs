using System.Security.Claims;
using CrmApp.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace CrmApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}

public class LanguageController : Controller
{
    [HttpGet("/language/{culture}")]
    public IActionResult Set(string culture, string? returnUrl = null)
    {
        culture = culture.ToLowerInvariant() switch
        {
            "es" => "es",
            "it" => "it",
            _ => "en"
        };

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            $"c={culture}|uic={culture}",
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, SameSite = SameSiteMode.Lax });

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");

        return Redirect("/");
    }
}
