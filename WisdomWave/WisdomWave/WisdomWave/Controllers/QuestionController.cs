using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using WisdomWave.Models;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService questionService;
        private readonly TestService testService;
        private readonly AnswerService answerService;

        public QuestionController(QuestionService questionService, TestService testService, AnswerService answerService)
        {
            this.questionService = questionService;
            this.testService = testService;
            this.answerService = answerService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all questions
        public async Task<IActionResult> Get()
        {
            var questions = await questionService.GetAsyncs();
            return new JsonResult(questions);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a question by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var question = await questionService.FindByConditionItemAsync(q => q.Id == id);
            if (question is null)
            {
                return NotFound();
            }
            return new JsonResult(question);
        }

        [HttpGet("GetQuestionAnswers/{id}")] // HTTP GET request handler for retrieving a question by its identifier
        public async Task<IActionResult> GetQuestionAnswers(int id)
        {
            var answers = await answerService.FindByConditionAsync(a => a.questionId == id);
            if (answers is null)
            {
                return NotFound();
            }
            return new JsonResult(answers);
        }

        [HttpPost] // HTTP POST request handler for creating a new question
        public async Task<IActionResult> Post([FromBody] PushQuestion question)
        {
            if (question is null)
            {
                return BadRequest();
            }

            var test = await testService.FindByConditionItemAsync(c => c.Id == question.testId);

            if (test is null)
            {
                return NotFound("Test not found");
            }


            var result = await questionService.CreateAsync(new Question
            {
                QuestionName = question.QuestionName,
                QuestionText= question.QuestionText,
                QuestionType = question.QuestionType,
                }, test.Id);

            if (!result.IsError)
            {
                return Created($"api/questions/{question.Id}", question); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing question
        public async Task<IActionResult> Put(int id, [FromBody] PushQuestion question)
        {
            if (question is null)
            {
                return BadRequest();
            }
            if(question.QuestionType != (await questionService.FindByConditionItemAsync(q=>q.Id==id)).QuestionType)
            {
                var answers = await answerService.FindByConditionAsync(a=>a.questionId==id);
                foreach(var answer in answers) {
                    try
                    {
                        await answerService.DeleteAsync(answer.Id);
                    }
                    catch(Exception e) {
                        return BadRequest();
                    }
                }
            }

            if(question.CountOfPoints == 0 || question.CountOfPoints == null)
            {
                question.CountOfPoints = 1;
            }

            await questionService.EditAsync(id, 
                new Question
                {
                    Id = question.Id,
                    QuestionName = question.QuestionName,
                    QuestionText = question.QuestionText,
                    QuestionType = question.QuestionType,
                    CountOfPoints = question.CountOfPoints,
                    testId = question.testId,
                    Test = await testService.FindByConditionItemAsync(t=>t.Id==question.testId)
                });
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
