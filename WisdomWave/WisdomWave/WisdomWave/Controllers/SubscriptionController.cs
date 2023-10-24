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
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService subscriptionService;

        public SubscriptionController(SubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех подписок
        public async Task<IActionResult> Get()
        {
            var subscriptions = await subscriptionService.GetAsyncs();
            return Ok(subscriptions);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения подписки по её идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await subscriptionService.FindByConditionItemAsync(s => s.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания новой подписки
        public async Task<IActionResult> Post([FromBody] Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest();
            }

            var result = await subscriptionService.CreateAsync(subscription);
            if (result.IsError == false)
            {
                return Created($"api/subscriptions/{subscription.Id}", subscription); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующей подписки
        public async Task<IActionResult> Put(int id, [FromBody] Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest();
            }

            await subscriptionService.EditAsync(id, subscription);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления подписки по её идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await subscriptionService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
