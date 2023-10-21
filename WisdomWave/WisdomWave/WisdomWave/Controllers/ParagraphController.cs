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
    public class ParagraphController : ControllerBase
    {
        private readonly ParagraphService paragraphService;

        public ParagraphController(ParagraphService paragraphService)
        {
            this.paragraphService = paragraphService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех параграфов
        public async Task<IActionResult> Get()
        {
            var paragraphs = await paragraphService.GetAsyncs();
            return Ok(paragraphs);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения параграфа по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var paragraph = await paragraphService.FindByConditionItemAsync(p => p.Id == id);
            if (paragraph == null)
            {
                return NotFound();
            }
            return Ok(paragraph);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового параграфа
        public async Task<IActionResult> Post([FromBody] Paragraph paragraph)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }

            var result = await paragraphService.CreateAsync(paragraph);
            if (result.Succeeded)
            {
                return Created($"api/paragraphs/{paragraph.Id}", paragraph); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего параграфа
        public async Task<IActionResult> Put(int id, [FromBody] Paragraph paragraph)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }

            await paragraphService.EditAsync(id, paragraph);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления параграфа по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await paragraphService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
