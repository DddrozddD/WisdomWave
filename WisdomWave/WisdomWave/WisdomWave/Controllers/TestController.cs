using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Specify that this is an API controller
    public class TestController : ControllerBase
    {
        private readonly TestService testService;
        private readonly UnitService unitService;
        private readonly QuestionService questionService;
        private readonly UserManager<WwUser> _userManager;

        public TestController(TestService testService, UserManager<WwUser> userManager, UnitService unitService, QuestionService questionService)
        {
            _userManager = userManager;
            this.testService = testService;
            this.unitService = unitService;
            this.questionService = questionService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all tests
        public async Task<IActionResult> Get()
        {
            var tests = await testService.GetAsyncs();
            return new JsonResult(tests);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a test by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var test = await testService.FindByConditionItemAsync(t => t.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            return new JsonResult(test);
        }

        [HttpGet("GetQuestions/{id}")]
        public async Task<IActionResult> GetQuestions(int id)
        {
            var questions = await questionService.FindByConditionAsync(q => q.testId == id);
            if(questions.Count() == 0)
            {
                var res = await questionService.CreateAsync(new Question
                {
                    QuestionName = "Питання тесту",
                    QuestionType ="",
                    QuestionText = "",
                    CountOfPoints = 1
                   }, id);

                if(res.IsError == false)
                {
                    questions = await questionService.FindByConditionAsync(q => q.testId == id);
                }
            }
            return new JsonResult(questions);
        }

        [HttpGet("{userid}/{testId}")] // HTTP GET request handler for retrieving a test by its identifier
        public async Task<IActionResult> Check(int testId, string userId)
        {

            var test = await testService.FindByConditionItemAsync(t => t.Id == testId);
            if (test == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await testService.CheckUser(test, user);
            if (result.IsError == false)
            {
                return Created($"api/tests/{test.Id}", test); // Return a status of 201 Created
            }
            return new JsonResult(result.Message);
        }
        
        [HttpPost()] // HTTP POST request handler for creating a new test
        public async Task<IActionResult> Post([FromBody] PostTest test)
        {
            if (test == null)
            {
                return BadRequest();
            }

            Unit unit = await unitService.FindByConditionItemAsync(u => u.Id == test.unitId);

            if (unit == null)
            {
                return NotFound("Unit not found");
            }

            Test newTest = new Test
            {
                Unit = unit,
                unitId = test.unitId,
                TestName = test.TestName,
                TestDescription = test.TestDescription,
                DateOfCreate = DateTime.Today.ToString()
            };


            var result = await testService.CreateAsync(newTest, unit.Id);
            if (result.IsError == false)
            {
                return Ok(); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing test
        public async Task<IActionResult> Put(int id, [FromBody] Test test)
        {
            if (test == null)
            {
                return BadRequest();
            }

            await testService.EditAsync(id, test);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a test by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await testService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
