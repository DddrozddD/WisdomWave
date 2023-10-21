using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly CourseService courseService;

        public AnswerController(CourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await courseService.GetAsyncs();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await courseService.FindByConditionItemAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            var result = await courseService.CreateAsync(course);
            if (result.Succeeded)
            {
                return Created($"api/courses/{course.Id}", course);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            await courseService.EditAsync(id, course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await courseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
