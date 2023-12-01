﻿using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Resume.Controllers
{
    public class EmailConfirmController : Controller
    {
        private readonly UserManager<WwUser> _userManager;

        public EmailConfirmController(UserManager<WwUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Confirm(string guid, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var res = await _userManager.ConfirmEmailAsync(user, guid);
                if (res.Succeeded)
                {
                    return RedirectToAction("Confirm", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
