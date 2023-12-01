using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;
using DAL.Models;
using System.Globalization;
using System.Linq;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class UnitController : ControllerBase
    {
        private readonly UnitService unitService;
        private readonly CourseService courseService;
        private readonly ParagraphService paragraphService;
        private readonly TestService testService;
        private readonly PageService pageService;
        private readonly UserManager<WwUser> _userManager;

        public UnitController(UnitService unitService, CourseService courseService, ParagraphService paragraphService, TestService testService, PageService pageService)
        {
            this.courseService = courseService;
            this.pageService= pageService;
            this.unitService = unitService;
            this.paragraphService = paragraphService;
            this.testService = testService;
        }


        [HttpGet] // HTTP GET request handler for retrieving all units
        public async Task<IActionResult> Get()
        {
            var units = await unitService.GetAsyncs();
            return new JsonResult(units);
        }

        [HttpGet("GetAllTestsOfUnit/{id}")] // HTTP GET request handler for retrieving a unit by its identifier
        public async Task<IActionResult> GetAllTestsOfUnit(int id)

        {
            
            IReadOnlyCollection<Test> tests = await testService.FindByConditionAsync(t=>t.unitId==id);
            if (tests == null)
            {
                return NotFound();
            }
            return new JsonResult(tests);
        }



        [HttpGet("GetAllPagesOfUnit/{id}")] // HTTP GET request handler for retrieving a unit by its identifier
        public async Task<IActionResult> GetAllPagesOfUnit(int id)
        {

            IReadOnlyCollection<Page> pages = await pageService.FindByConditionAsync(t => t.unitId == id);
            if (pages == null)
            {
                return NotFound();
            }
            return new JsonResult(pages);
        }

        [HttpGet("Check/{userId}/{unitId}")] // HTTP GET request handler for retrieving a test by its identifier
        public async Task<IActionResult> Check(int unitId, string userId)
        {

            var unit = await unitService.FindByConditionItemAsync(t => t.Id == unitId);
            if (unit == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await unitService.CheckUser(unit, user);
            if (result.IsError == false)
            {
                return Created($"api/tests/{unit.Id}", unit); // Return a status of 201 Created
            }
            return new JsonResult(result.Message);
        }

        [HttpPost("{courseId}")] // HTTP POST request handler for creating a new unit
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

        [HttpPost("addDefaultUnit/{courseId}")] // HTTP POST request handler for creating a new default unit
        public async Task<IActionResult> Post(int courseId)
        {
            Unit unit = new Unit { Number = 1, DateOfCreate = DateTime.Now.ToString(), UnitName = "Новий блок" };
            var result = await unitService.CreateAsync(unit, courseId);

            // Get the course by CourseId using CourseService
           
            if (result.IsError == false)
            {
                return Ok(); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing unit
        public async Task<IActionResult> Put(int id, [FromBody] ReturnUnit unit)
        {
            if (unit == null)
            {
                return BadRequest();
            }

            Unit thisUnit = await unitService.FindByConditionItemAsync(u => u.Id == id);
            OperationDetails res = await unitService.EditAsync(id, new Unit
            {
                Id = unit.Id,
                UnitName = unit.UnitName,
                DateOfCreate = unit.DateOfCreate,
                Number= unit.Number,
                courseId = unit.courseId,
                Course = thisUnit.Course,
                Pages = thisUnit.Pages,
                Tests = thisUnit.Tests,
                PassedUnitUsers = thisUnit.PassedUnitUsers
            });
           
            if (res.IsError == false)
            {
                return Ok();
            }
            else
            {
                return BadRequest(res.Message);
            }
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a unit by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await unitService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
