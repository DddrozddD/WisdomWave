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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService questionService;
        private readonly TestService testService;

        public QuestionController(QuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all questions
        public async Task<IActionResult> Get()
        {
            var questions = await questionService.GetAsyncs();
            return Ok(questions);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a question by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var question = await questionService.FindByConditionItemAsync(q => q.Id == id);
            if (question is null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost("testId")] // HTTP POST request handler for creating a new question
        public async Task<IActionResult> Post([FromBody] Question question, int testId)
        {
            if (question is null)
            {
                return BadRequest();
            }

            var test = await testService.FindByConditionItemAsync(c => c.Id == testId);

            if (test is null)
            {
                return NotFound("Test not found");
            }

            question.Test = test;

            var result = await questionService.CreateAsync(question, test.Id);
            if (!result.IsError)
            {
                return Created($"api/questions/{question.Id}", question); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing question
        public async Task<IActionResult> Put(int id, [FromBody] Question question)
        {
            if (question is null)
            {
                return BadRequest();
            }

            await questionService.EditAsync(id, question);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a question by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await questionService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
