using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubQuestionController : ControllerBase
    {
        private readonly SubQuestionService subQuestionService;
        private readonly QuestionService questionService;

        public SubQuestionController(SubQuestionService subQuestionService)
        {
            this.subQuestionService = subQuestionService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all sub-questions
        public async Task<IActionResult> Get()
        {
            var subQuestions = await subQuestionService.GetAsyncs();
            return new JsonResult(subQuestions);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a sub-question by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var subQuestion = await subQuestionService.FindByConditionItemAsync(s => s.Id == id);
            if (subQuestion == null)
            {
                return NotFound();
            }
            return new JsonResult(subQuestion);
        }

        [HttpPost("questionId")] // HTTP POST request handler for creating a new sub-question
        public async Task<IActionResult> Post([FromBody] SubQuestion subQuestion, int questionId)
        {
            if (subQuestion == null)
            {
                return BadRequest();
            }

            var question = await questionService.FindByConditionItemAsync(sq => sq.Id == questionId);

            if (question == null)
            {
                return NotFound("Question not found");
            }
            subQuestion.Question = question;

            var result = await subQuestionService.CreateAsync(subQuestion, question.Id);
            if (result.IsError == false)
            {
                return Created($"api/subquestions/{subQuestion.Id}", subQuestion); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing sub-question
        public async Task<IActionResult> Put(int id, [FromBody] SubQuestion subQuestion)
        {
            if (subQuestion == null)
            {
                return BadRequest();
            }

            await subQuestionService.EditAsync(id, subQuestion);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a sub-question by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await subQuestionService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
