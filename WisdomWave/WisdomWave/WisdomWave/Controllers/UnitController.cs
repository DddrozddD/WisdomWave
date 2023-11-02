using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class UnitController : ControllerBase
    {
        private readonly UnitService unitService;
        private readonly CourseService courseService;

        public UnitController(UnitService unitService)
        {
            this.unitService = unitService;
        }


        [HttpGet] // HTTP GET request handler for retrieving all units
        public async Task<IActionResult> Get()
        {
            var units = await unitService.GetAsyncs();
            return new JsonResult(units);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a unit by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var unit = await unitService.FindByConditionItemAsync(u => u.Id == id);
            if (unit == null)
            {
                return NotFound();
            }
            return new JsonResult(unit);
        }

        [HttpPost("courseId")] // HTTP POST request handler for creating a new unit
        public async Task<IActionResult> Post([FromBody] Unit unit, int courseId)
        {
            if (unit == null)
            {
                return BadRequest();
            }

            // Get the course by CourseId using CourseService
            var course = await courseService.FindByConditionItemAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            unit.Course = course; // Set the Course property

            var result = await unitService.CreateAsync(unit, courseId);
            if (result.IsError == false)
            {
                return Created($"api/units/{unit.Id}", unit); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing unit
        public async Task<IActionResult> Put(int id, [FromBody] Unit unit)
        {
            if (unit == null)
            {
                return BadRequest();
            }

            await unitService.EditAsync(id, unit);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a unit by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await unitService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
