using System.Security.Claims;

namespace CrmApp.Services;

public static class ClaimsExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(value, out var id) ? id : 0;
    }

    public static bool GetIsAdmin(this ClaimsPrincipal principal) =>
        principal.FindFirstValue("IsAdmin") == "true";
}
