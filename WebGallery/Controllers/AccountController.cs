using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Entities.Identity;
using WebGallery.Models;
using WebGallery.Services;

namespace WebGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser>  _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Not Found");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false, false);
            if (!result.Succeeded)
                return BadRequest("Not Found");

            return Ok(
                new
                {
                    token = _jwtTokenService.CreateToken(user)
                });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return BadRequest("Email is use");

            user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = _userManager.CreateAsync(user, model.Password).Result;
            //result = _userManager.AddToRoleAsync(user, Roles.Admin).Result;
            return Ok();
        }
    }
}
