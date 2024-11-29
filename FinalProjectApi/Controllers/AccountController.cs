using AutoMapper;
using FinalProject.Core.Models.Identity;
using FinalProject.Core.Services.Contract;
using FinalProjectApi.Dtos;
using FinalProjectApi.Errors;
using FinalProjectApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , IAuthService  authService , IMapper mapper)
        {
            _userManager = userManager;
           _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTo model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(result.IsCompletedSuccessfully is false) if (user == null) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user ,_userManager)
            });

           
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value) 
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] {"This Email is Already Exist !"} });
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user , model.Password);

            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =  await _authService.CreateTokenAsync (user ,_userManager)
            });

           
        }

        [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]
        public async Task < ActionResult<Address>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUsersWithAddressByEmail(User);
            return Ok(user.Address);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")]
        public async Task <ActionResult<Address>>UpdateUserAddress(AddressDto address)
        {
            var updateAddress = _mapper.Map<AddressDto , Address>(address);
            var user = await _userManager.FindUsersWithAddressByEmail(User);
            updateAddress.Id = user.Address.Id;
            user.Address = updateAddress;
            var result = _userManager.UpdateAsync(user);
            if (result.IsCompletedSuccessfully) 
            
                return BadRequest(400);

            return Ok(address);

            
        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
