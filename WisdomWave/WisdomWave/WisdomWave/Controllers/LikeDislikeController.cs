﻿using System;
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
    public class LikeDislikeController : ControllerBase
    {
        private readonly LikeDislikeService likeDislikeService;
        private readonly ReviewService reviewService;

        public LikeDislikeController(LikeDislikeService likeDislikeService)
        {
            this.likeDislikeService = likeDislikeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var likeDislikes = await likeDislikeService.GetAsyncs();
            return new JsonResult(likeDislikes);
        }

        [HttpGet("{userId}/{reviewId}")]
        public async Task<IActionResult> Get(string userId, int reviewId)
        {
            var likeDislike = await likeDislikeService.FindByConditionItemAsync(ld => ld.userId == userId && ld.reviewId == reviewId);
            if (likeDislike == null)
            {
                return NotFound();
            }
            return new JsonResult(likeDislike);
        }

        [HttpPost("reviewId")]
        public async Task<IActionResult> Post([FromBody] LikeDislike likeDislike, int reviewId)
        {
            if (likeDislike == null)
            {
                return BadRequest();
            }

            var review = await reviewService.FindByConditionItemAsync(r => r.Id == reviewId);

            if (review == null)
            {
                return NotFound("Review not found");
            }

            likeDislike.Review = review;

            var result = await likeDislikeService.CreateAsync(likeDislike, review.Id);
            if (result.IsError == false)
            {
                return Created($"api/likedislikes/{likeDislike.userId}/{likeDislike.reviewId}", likeDislike);
            }
            return BadRequest(result.Message);
        }
            
        [HttpPut("{userId}/{reviewId}")]
        public async Task<IActionResult> Put(string userId, int reviewId, [FromBody] LikeDislike likeDislike)
        {
            if (likeDislike == null)
            {
                return BadRequest();
            }

            await likeDislikeService.EditAsync(userId, reviewId, likeDislike);
            return NoContent();
        }

        [HttpDelete("{userId}/{reviewId}")]
        public async Task<IActionResult> Delete(string userId, int reviewId)
        {
            await likeDislikeService.DeleteAsync(userId, reviewId);
            return NoContent();
        }
    }
}
