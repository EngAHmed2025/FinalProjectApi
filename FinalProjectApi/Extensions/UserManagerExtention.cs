using FinalProject.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProjectApi.Extensions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindUsersWithAddressByEmail (this UserManager<AppUser> userManager , ClaimsPrincipal user  )
        {
            var email =  user.FindFirstValue(ClaimTypes.Email);  
            var User  = userManager.Users.Include(U => U.Address).FirstOrDefault(u =>u.NormalizedEmail == email.ToUpper());
            return User;
        }
    }
}
