using FinalProject.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(AppUser appUser , UserManager<AppUser>userManager);


    }
}
