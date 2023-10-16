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
    public class SubQuestionController : ControllerBase
    {
        private readonly SubQuestionServiceDAL subQuestionService;

        public SubQuestionController(SubQuestionServiceDAL subQuestionService)
        {
            this.subQuestionService = subQuestionService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех подвопросов
        public async Task<IActionResult> Get()
        {
            var subQuestions = await subQuestionService.GetAsyncs();
            return Ok(subQuestions);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения подвопроса по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var subQuestion = await subQuestionService.FindByConditionItemAsync(s => s.Id == id);
            if (subQuestion == null)
            {
                return NotFound();
            }
            return Ok(subQuestion);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового подвопроса
        public async Task<IActionResult> Post([FromBody] SubQuestion subQuestion)
        {
            if (subQuestion == null)
            {
                return BadRequest();
            }

            var result = await subQuestionService.CreateAsync(subQuestion);
            if (result.Succeeded)
            {
                return Created($"api/subquestions/{subQuestion.Id}", subQuestion); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего подвопроса
        public async Task<IActionResult> Put(int id, [FromBody] SubQuestion subQuestion)
        {
            if (subQuestion == null)
            {
                return BadRequest();
            }

            await subQuestionService.EditAsync(id, subQuestion);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления подвопроса по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await subQuestionService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
