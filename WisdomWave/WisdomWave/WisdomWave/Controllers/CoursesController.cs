using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService;
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
}
