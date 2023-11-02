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
    [ApiController] // Specify that this is an API controller
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService reviewService;
        private readonly CourseService courseService;

        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all reviews
        public async Task<IActionResult> Get()
        {
            var reviews = await reviewService.GetAsyncs();
            return Ok(reviews);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a review by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var review = await reviewService.FindByConditionItemAsync(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost("courseId")] // HTTP POST request handler for creating a new review
        public async Task<IActionResult> Post([FromBody] Review review, int courseId)
        {
            if (review == null)
            {
                return BadRequest();
            }

            var course = await courseService.FindByConditionItemAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            review.Course = course;

            var result = await reviewService.CreateAsync(review, course.Id);
            if (result.IsError == false)
            {
                return Created($"api/reviews/{review.Id}", review); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing review
        public async Task<IActionResult> Put(int id, [FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            await reviewService.EditAsync(id, review);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a review by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await reviewService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
