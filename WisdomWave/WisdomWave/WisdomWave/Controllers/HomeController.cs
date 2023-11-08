using System.Diagnostics;
using BLL.Services;
using Domain.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WisdomWave.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SubscriptionService _subscriptionService;
    private readonly CategoryService _categoryService;
    private readonly CourseService _courseService;
    private readonly UserManager<User> _userManager;
    public HomeController(ILogger<HomeController> logger, SubscriptionService subscriptionService, CourseService courseService, UserManager<User> userManager, CategoryService categoryService)
    {
        _subscriptionService = subscriptionService;
        _logger = logger;
        _courseService = courseService;
        _userManager = userManager;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        _courseService.FindAllLearningCoursesForUser((_userManager.FindByNameAsync(User.Identity.Name)).Id.ToString());
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

    public async Task<IActionResult> ShowViewReg()
    {
        
        Category newCategory2 = new Category();
        newCategory2.CategoryName = "Some2";
        Category newCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Some");
        await _categoryService.CreateAsync(newCategory2, newCategory.Id);

        return View("Registration");
    }
    public IActionResult ShowLoginReg()
    {
        return View("Login");
    }

}
