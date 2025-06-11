using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurniStyle.API.Extensions
{
    public static class UserManagerExtensions
    {
       public static async Task<ApplicationUser> FindByEmailWithAddressAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal user)
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return null;
            var users = await userManager.Users.Include(p=>p.Address).FirstOrDefaultAsync(p=>p.Email== userEmail);
            if(users == null) return null;
            return users;
        }

    }
}
