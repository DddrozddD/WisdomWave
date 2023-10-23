using ASP_Resume.Models;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_Resume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthorizationController : ControllerBase
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ApiAuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;

        }



     

            _configuration = configuration;
            _env = env;
        }


       

        [HttpPost("RegUser")]
        public async Task<IList<IdentityError>> RegUser([FromBody] RegisterViewModel registerViewModel)
        {
           
            if (registerViewModel.ConfirmPass == registerViewModel.Password) { 
            var user = new User
            {
                Email = registerViewModel.Email, 

                Surname= registerViewModel.Surname,
                Telephone= registerViewModel.Telephone,
                UserName = registerViewModel.Name

            };

            var res = await _userManager.CreateAsync(user, registerViewModel.Password);
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
                    return null;

            }
                else
                {
                    return res.Errors.ToList();
                }
            }
            var error = new List<IdentityError>();
            error.Add(new IdentityError() { Code="Fail passwords", Description= "Fail passwords" });
            return error;
        }

       [HttpPost("LoginUser")]
        public async Task<string> Login([FromBody] LoginViewModel loginViewModel)
        {
            var tmpClient = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (tmpClient != null)
            {
                var res = await _signInManager.PasswordSignInAsync(tmpClient.UserName, loginViewModel.Password, true, false);
                if (res.Succeeded)
                {
                    return null;
                }
                return "User is not found";
            }
            return "User is not found";
        }



        [HttpGet("LogoutUser")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
