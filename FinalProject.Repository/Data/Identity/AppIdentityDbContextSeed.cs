using FinalProject.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Repository.Data.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed",
                    Email = "Ahmed.gamal@intalio.com",
                    UserName = "ahmed.gamal",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(user , "Pa$$sw0rd");
            }
        }
    }
}
