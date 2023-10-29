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
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService reviewService;
        private readonly CourseService courseService;

        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet] // Обработчик HTTP GET-запроса для получения всех отзывов
        public async Task<IActionResult> Get()
        {
            var reviews = await reviewService.GetAsyncs();
            return Ok(reviews);
        }

        [HttpGet("{id}")] // Обработчик HTTP GET-запроса для получения отзыва по его идентификатору
        public async Task<IActionResult> Get(int id)
        {
            var review = await reviewService.FindByConditionItemAsync(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost] // Обработчик HTTP POST-запроса для создания нового отзыва
        public async Task<IActionResult> Post([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            var course = await courseService.FindByConditionItemAsync(c => c.Id == review.courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            review.Course = course;

            var result = await reviewService.CreateAsync(review, course.Id);
            if (result.IsError == false)
            {
                return Created($"api/reviews/{review.Id}", review); // Возвращаем статус 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // Обработчик HTTP PUT-запроса для обновления существующего отзыва
        public async Task<IActionResult> Put(int id, [FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            await reviewService.EditAsync(id, review);
            return NoContent(); // Возвращаем статус 204 No Content
        }

        [HttpDelete("{id}")] // Обработчик HTTP DELETE-запроса для удаления отзыва по его идентификатору
        public async Task<IActionResult> Delete(int id)
        {
            await reviewService.DeleteAsync(id);
            return NoContent(); // Возвращаем статус 204 No Content
        }
    }
}
