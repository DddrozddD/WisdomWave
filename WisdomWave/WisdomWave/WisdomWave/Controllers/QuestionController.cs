using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Определяем базовый маршрут для контроллера
    [ApiController] // Указываем, что это контроллер API
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService questionService;
        private readonly TestService testService;

        public QuestionController(QuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех вопросов
        public async Task<IActionResult> Get()
        {
            var questions = await questionService.GetAsyncs();
            return Ok(questions);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения вопроса по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var question = await questionService.FindByConditionItemAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового вопроса
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            if (question == null)
            {
                return BadRequest();
            }

            var test = await testService.FindByConditionItemAsync(c => c.Id == question.testId);


            if (test == null)
            {
                return NotFound("Test not found");
            }

            question.Test = test;

            var result = await questionService.CreateAsync(question,test.Id);
            if (result.IsError == false)
            {
                return Created($"api/questions/{question.Id}", question); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего вопроса
        public async Task<IActionResult> Put(int id, [FromBody] Question question)
        {
            if (question == null)
            {
                return BadRequest();
            }

            await questionService.EditAsync(id, question);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления вопроса по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await questionService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
