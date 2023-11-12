﻿using System.Diagnostics;
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
        //_categoryService.CreateAsync(new Category { CategoryName = "Дизайн" });
        Category parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Дизайн одягу");
        await _categoryService.CreateAsync(new Category { CategoryName = "Мода" }, parentCategory.Id);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> ShowViewReg()
    {
        

        return View("Registration");
    }
    public IActionResult ShowLoginReg()
    {
        return View("Login");
    }

}
