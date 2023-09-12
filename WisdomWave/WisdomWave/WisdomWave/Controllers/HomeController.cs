using System.Diagnostics;
using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WisdomWave.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SubscriptionService _subscriptionService;
    public HomeController(ILogger<HomeController> logger, SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        Subscription s = new Subscription();
        s.Value = "some";
        s.userId = "ssdd";
        s.FavouriteTheme = "newTheme";
        s.AgreeForSub = 1;
        await _subscriptionService.CreateAsync(s);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ShowViewReg()
    {
        return View("Registration");
    }
    public IActionResult ShowLoginReg()
    {
        return View("Login");
    }

}
