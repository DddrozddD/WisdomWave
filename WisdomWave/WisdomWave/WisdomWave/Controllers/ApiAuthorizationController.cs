using ASP_Resume.Models;
using Domain.Models;
using Domain.Models.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_Resume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _configuration = configuration;
            _env = env;

        }



        
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                User user = _userManager.FindByIdAsync(UserIdentity.UserIdentityId).Result;
                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost("RegUser")]
        public async Task<IActionResult> RegUser([FromBody] RegisterViewModel registerViewModel)
        {
           
            if (registerViewModel.ConfirmPass == registerViewModel.Password) { 
            var user = new User
            {
                Email = registerViewModel.Email,
                UserName = "User"
            };

            var res = await _userManager.CreateAsync(user, registerViewModel.Password);
               /* user = _userManager.FindByEmailAsync(registerViewModel.Email).Result;
                user.UserName = "User" + user.Id;
                _userManager.UpdateAsync(user);*/
                if (res.Succeeded)
            {
                if (await _roleManager.FindByNameAsync("user") == null)
                {
                    var role = await _roleManager.CreateAsync(new IdentityRole("user"));
                    if (role.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "user");
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("", "confirmation", new { guid = token, userEmail = user.Email }, Request.Scheme, Request.Host.Value);
                await _emailSender.SendEmailAsync(user.Email, "Confirmation Link", $"Link=> {confirmationLink}");

                    return Created($"profile/userProfile", user);

            }
                else
                {
                    return BadRequest("Fail passwords");
                }
            }
            /*var error = new List<IdentityError>();
            error.Add(new IdentityError() { Code="Fail passwords", Description= "Fail passwords" });*/
            return BadRequest("Fail passwords");
        }

       [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            
            var tmpClient = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (tmpClient != null)
            {
                var res = await _signInManager.PasswordSignInAsync(tmpClient.UserName, loginViewModel.Password, true, false);
                if (res.Succeeded)
                {
                    User user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                    UserIdentity.UserIdentityId = user.Id;
                    return NoContent();
                }
                
                return BadRequest("User is not found");
            }
            return BadRequest("User is not found");
        }



        [HttpGet("LogoutUser")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
