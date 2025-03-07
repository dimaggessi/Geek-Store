using System.Security.Claims;

namespace GeekStore.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserEmail(this ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email)
            ?? null;

        return email;
    }
}
