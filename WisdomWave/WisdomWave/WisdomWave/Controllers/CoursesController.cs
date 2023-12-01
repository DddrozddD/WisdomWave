using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;
using Domain.Models.Helper;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly CategoryService _categoryService;
    private readonly UserManager<User> _userManager;

    public CoursesController(CourseService courseService, UserManager<User> userManager, CategoryService categoryService)
    {
        _courseService = courseService;
        _userManager = userManager;
        _categoryService = categoryService;
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
        User user = await _userManager.GetUserAsync(User);
        var course = await _courseService.FindByConditionItemAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return new JsonResult(course);
    }
    [HttpGet("GetEditCourse")]
    public async Task<IActionResult> GetEditCourse()
    {
        
        User user = await _userManager.GetUserAsync(User);
        var course = await _courseService.FindByConditionItemAsync(c => c.Id == UserIdentity.EditCourseId);

        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseForm courseForm)
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
            Course course = new Course()
            {
                DateOfCreate = DateTime.Now,
                CourseName = courseForm.CourseName,
                Description = courseForm.Description,
                Language = courseForm.Language,
                Categories = categories,
                CreatorUser = await _userManager.FindByIdAsync(UserIdentity.UserIdentityId),
                creatorUserId = UserIdentity.UserIdentityId
            };

            await _courseService.CreateAsync(course);
            course = await _courseService.FindByConditionItemAsync(c => c.CourseName == course.CourseName && c.creatorUserId == course.creatorUserId && c.DateOfCreate.Minute == course.DateOfCreate.Minute);
            UserIdentity.EditCourseId = course.Id;
            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        await _courseService.EditAsync(id, course);
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

    [HttpGet("user-courses/{userId}")]
    public async Task<IActionResult> GetUserCourses(string userId)
    {
        var courses = await _courseService.FindAllLearningCoursesForUser(userId);
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
