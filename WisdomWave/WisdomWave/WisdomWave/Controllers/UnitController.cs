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
    public class UnitController : ControllerBase
    {
        private readonly UnitService unitService;
        private readonly CourseService courseService;

        public UnitController(UnitService unitService)
        {
            this.unitService = unitService;
        }
    

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех юнитов
        public async Task<IActionResult> Get()
        {
            var units = await unitService.GetAsyncs();
            return Ok(units);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения юнита по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var unit = await unitService.FindByConditionItemAsync(u => u.Id == id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового юнита
        public async Task<IActionResult> Post([FromBody] Unit unit)
        {
            if (unit == null)
            {
                return BadRequest();
            }

            // Получаем курс по CourseId с использованием CourseService
            var course = await courseService.FindByConditionItemAsync(c => c.Id == unit.courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            unit.Course = course; // Устанавливаем свойство Course

            var result = await unitService.CreateAsync(unit, course.Id);
            if (result.IsError == false)
            {
                return Created($"api/units/{unit.Id}", unit); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Message);
        }



        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего юнита
        public async Task<IActionResult> Put(int id, [FromBody] Unit unit)
        {
            if (unit == null)
            {
                return BadRequest();
            }

            await unitService.EditAsync(id, unit);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления юнита по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await unitService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
        

    }
}
