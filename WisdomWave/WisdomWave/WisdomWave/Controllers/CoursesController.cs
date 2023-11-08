using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Identity;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly UserManager<User> _userManager;

    public CoursesController(CourseService courseService, UserManager<User> userManager)
    {
        _courseService = courseService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetAsyncs();
        return Ok(courses);
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

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        await _courseService.CreateAsync(course);
        return CreatedAtAction("GetCourse", new { id = course.Id }, course);
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
        return Ok(courses);
    }

    [HttpGet("user-courses/{userId}")]
    public async Task<IActionResult> GetUserCourses(string userId)
    {
        var courses = await _courseService.FindAllLearningCoursesForUser(userId);
        return Ok(courses);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCourses([FromQuery] string searchTerm)
    {
        var courses = await _courseService.SearchCoursesAsync(searchTerm);
        return Ok(courses);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCourses([FromQuery] int minRating)
    {
        var courses = await _courseService.FilterCoursesByRatingAsync(minRating);
        return Ok(courses);
    }

    [HttpGet("search-by-description")]
    public async Task<IActionResult> SearchCoursesByDescription([FromQuery] string description)
    {
        var courses = await _courseService.SearchCoursesByDescriptionAsync(description);
        return Ok(courses);
    }

    [HttpGet("search-by-creator")]
    public async Task<IActionResult> SearchCoursesByCreator([FromQuery] string creatorUserId)
    {
        var courses = await _courseService.SearchCoursesByCreatorAsync(creatorUserId);
        return Ok(courses);
    }

    [HttpGet("filter-by-rating")]
    public async Task<IActionResult> FilterCoursesByRating([FromQuery] int minRating)
    {
        var courses = await _courseService.FilterCoursesByRatingAsync(minRating);
        return Ok(courses);
    }
}
