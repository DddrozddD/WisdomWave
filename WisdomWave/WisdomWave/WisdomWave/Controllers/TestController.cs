using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Определяем базовый маршрут для контроллера
    [ApiController] // Указываем, что это контроллер API
    public class TestController : ControllerBase
    {
        private readonly TestService testService;

        public TestController(TestService testService)
        {
            this.testService = testService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех тестов
        public async Task<IActionResult> Get()
        {
            var tests = await testService.GetAsyncs();
            return Ok(tests);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения теста по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var test = await testService.FindByConditionItemAsync(t => t.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового теста
        public async Task<IActionResult> Post([FromBody] Test test)
        {
            if (test == null)
            {
                return BadRequest();
            }

            var result = await testService.CreateAsync(test);
            if (result.IsError == false)
            {
                return Created($"api/tests/{test.Id}", test); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего теста
        public async Task<IActionResult> Put(int id, [FromBody] Test test)
        {
            if (test == null)
            {
                return BadRequest();
            }

            await testService.EditAsync(id, test);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления теста по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await testService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
