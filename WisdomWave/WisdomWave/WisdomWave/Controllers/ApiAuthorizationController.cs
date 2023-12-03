using ASP_Resume.Models;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;
using WisdomWave.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_Resume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<WwUser> _userManager;
        private readonly SignInManager<WwUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AuthorizationController(UserManager<WwUser> userManager, SignInManager<WwUser> signInManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _configuration = configuration;
            _env = env;
        }




        [HttpGet("GetUser/{token}")]
        public async Task<IActionResult> GetUser(string token)
        {
            var claims = JwtHandler.DecodeJwtToken(token);

            WwUser userIdClaim = await _userManager.FindByIdAsync(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);


            if (userIdClaim != null)
            {

                return new JsonResult(userIdClaim);
            }

            return BadRequest();
        }



        [HttpPost("RegUser")]
        public async Task<IActionResult> RegUser([FromBody] RegisterViewModel registerViewModel)
        {

            if (registerViewModel.ConfirmPass == registerViewModel.Password)
            {
                var user = new WwUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    Name = registerViewModel.Name,
                    Surname = registerViewModel.Surname
                };

                var res = await _userManager.CreateAsync(user, registerViewModel.Password);
                /* user = await _userManager.FindByEmailAsync(registerViewModel.Email);
                 user.UserName = "User" + user.Id;
                 await _userManager.UpdateAsync(user);*/
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
                    await _emailSender.SendEmailAsync(user.Email, "Confirmation Link", $"Click to confirmation your email: \n {confirmationLink}");

                    return Created($"profile/userProfile/", user);

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
                    var token = JwtHandler.GenerateJwtToken(tmpClient);
                    /*User user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                    UserIdentity.UserIdentityId = user.Id;*/
                    return Ok(token);
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
