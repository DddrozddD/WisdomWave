using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using EllipticCurve.Utils;
using System.Security.Claims;
using NuGet.Common;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly CategoryService _categoryService;
    private readonly UserManager<WwUser> _userManager;
    private readonly UnitService _unitService;
    private readonly TestService _testService;
    private readonly ParagraphService _paragraphService;
    private readonly PageService _pageService;

    public CoursesController(CourseService courseService, UserManager<WwUser> userManager, CategoryService categoryService, UnitService unitService, TestService testService, ParagraphService paragraphService,
        PageService pageService)
    {
        _courseService = courseService;
        _userManager = userManager;
        _categoryService = categoryService;
        _unitService = unitService;
        _testService = testService;
        _paragraphService = paragraphService;
        _pageService = pageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetAsyncs();
        return new JsonResult(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _courseService.FindByConditionItemAsync(c => c.Id == id);
        course.Categories = await _categoryService.FindByConditionAsync(c => c.Courses.Any(c=>c.Id== id));
        course.LearnerUsers = await _userManager.Users.Where(u => u.LearningCourses.Any(c => c.Id == id)).ToListAsync();
        course.CompletedUsers = await _userManager.Users.Where(u => u.CompletedCourses.Any(c => c.Id == id)).ToListAsync();

        if (course == null)
        {
            return NotFound();
        }

        return new JsonResult(course);
    }



    [HttpGet("checkCourseOfCreator/{id}/{token}")]
    public async Task<IActionResult> checkCourseOfCreator(int id, string token)
    {
        WwUser User = await _userManager.FindByIdAsync(JwtHandler.DecodeJwtToken(token).FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        Course Course = await _courseService.FindByConditionItemAsync(c => c.Id == id);

        if (User == null || Course == null)
        {
            return NotFound();
        }

        if (User.Id == Course.creatorUserId)
        {
            return new JsonResult(true);
        }
        return new JsonResult(false);
    }

    [HttpGet("GetEditCourseUnits/{courseId}")]
    public async Task<IActionResult> GetEditCourseUnits(int courseId)
    {
        var course = await _courseService.FindByConditionItemAsync(c => c.Id == courseId);
        IReadOnlyCollection<Unit> courseUnits = await _unitService.FindByConditionAsync(u => u.courseId == course.Id);


        var returnUnits = new List<ReturnUnit>();
        if (course == null)
        {
            return NotFound();
        }
        if (courseUnits.Count == 0)
        {
            await _unitService.CreateAsync(new Unit { Number = 1, DateOfCreate = DateTime.Now.ToString(), UnitName = "Новий блок" }, course.Id);
            course = await _courseService.FindByConditionItemAsync(c => c.Id == courseId);

            foreach (var unit in course.Units)
            {
                ReturnUnit returnUnit = new ReturnUnit()
                {

                    courseId = unit.courseId,
                    DateOfCreate = unit.DateOfCreate.ToString(),
                    UnitName = unit.UnitName,
                    Id = unit.Id,
                    Number = unit.Number
                };
                returnUnits.Add(returnUnit);
            }
            return new JsonResult(returnUnits);
        }
        foreach (var unit in courseUnits)
        {
            ReturnUnit returnUnit = new ReturnUnit()
            {

                courseId = unit.courseId,
                DateOfCreate = course.DateOfCreate.ToString(),
                UnitName = unit.UnitName,
                Id = unit.Id,
                Number = unit.Number

            };
            returnUnits.Add(returnUnit);
        }

        return new JsonResult(returnUnits);
    }



    [HttpGet("GetUserCourses/{token}")]
    public async Task<IActionResult> GetUserCourses(string token)
    {
        var claims = JwtHandler.DecodeJwtToken(token);

        string userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var courses = await _courseService.FindByConditionAsync(c => c.creatorUserId == userIdClaim);

        if (courses == null)
        {
            return NotFound();
        }

        return new JsonResult(courses);
    }

    [HttpPost("{userToken}")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseForm courseForm, string userToken)
    {
        try
        {
            Category knowlage = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == courseForm.Knowledge);
            Category education = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == courseForm.Education);
            Category theme = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == courseForm.Theme);
            List<Category> categories = new List<Category>();
            categories.Add(knowlage);
            categories.Add(education);
            categories.Add(theme);
            var claims = JwtHandler.DecodeJwtToken(userToken);

            WwUser User = await _userManager.FindByIdAsync(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Course course = new Course()
            {
                DateOfCreate = DateTime.Now,
                CourseName = courseForm.CourseName,
                Description = courseForm.Description,
                Language = courseForm.Language,
                Categories = categories,
                CreatorUser = User,
                creatorUserId = User.Id,
                creatorUserName = User.Name + " " + User.Surname
            };

            var result = await _courseService.CreateAsync(course);


            if (result.IsError == false)
            {

                return Ok();
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CreateCourseForm course)
    {
        Category knowlage = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == course.Knowledge);
        Category education = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == course.Education);
        Category theme = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == course.Theme);
        List<Category> categories = new List<Category>();
        categories.Add(knowlage);
        categories.Add(education);
        categories.Add(theme);

        var thisCourse = await _courseService.FindByConditionItemAsync(c=>c.Id==id);
        thisCourse.CourseName = course.CourseName;
        thisCourse.Description = course.Description;
        thisCourse.Language = course.Language;
        thisCourse.Categories = categories;


        await _courseService.EditAsync(id, thisCourse);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _courseService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("all-courses")]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseService.GetAsyncs();
        return new JsonResult(courses);
    }



    [HttpGet("search")]
    public async Task<IActionResult> SearchCourses([FromQuery] string searchTerm)
    {
        var courses = await _courseService.SearchCoursesAsync(searchTerm);
        return new JsonResult(courses);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCourses([FromQuery] int minRating)
    {
        var courses = await _courseService.FilterCoursesByRatingAsync(minRating);
        return new JsonResult(courses);
    }

    [HttpGet("search-by-description")]
    public async Task<IActionResult> SearchCoursesByDescription([FromQuery] string description)
    {
        var courses = await _courseService.SearchCoursesByDescriptionAsync(description);
        return new JsonResult(courses);
    }

    [HttpGet("search-by-creator")]
    public async Task<IActionResult> SearchCoursesByCreator([FromQuery] string creatorUserId)
    {
        var courses = await _courseService.SearchCoursesByCreatorAsync(creatorUserId);
        return new JsonResult(courses);
    }

    [HttpGet("filter-by-rating")]
    public async Task<IActionResult> FilterCoursesByRating([FromQuery] int minRating)
    {
        var courses = await _courseService.FilterCoursesByRatingAsync(minRating);
        return new JsonResult(courses);
    }
}
