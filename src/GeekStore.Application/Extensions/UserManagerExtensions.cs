using GeekStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GeekStore.Application.Extensions;
public static class UserManagerExtensions
{
    public static async Task<ApplicationUser?> GetUserWithAddressByEmail(this UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user)
    {
        return await userManager.Users
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u =>
                u.Email == user.FindFirstValue(ClaimTypes.Email));
    }
}
