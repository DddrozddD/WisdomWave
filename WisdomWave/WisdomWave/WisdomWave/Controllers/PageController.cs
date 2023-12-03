﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class PageController : ControllerBase
    {
        private readonly PageService pageService;
        private readonly ParagraphService paragraphService;
        private readonly UnitService unitService;
        private readonly UserManager<WwUser> _userManager;

        public PageController(PageService pageService, UnitService unitService, ParagraphService paragraphService)
        {
            this.unitService = unitService;
            this.pageService = pageService;
            this.paragraphService = paragraphService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all Pages
        public async Task<IActionResult> Get()
        {
            var Pages = await pageService.GetAsyncs();
            return new JsonResult(Pages);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a Page by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var page = await pageService.FindByConditionItemAsync(p => p.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return new JsonResult(page);
        }

        [HttpGet("GetParagraphsOfPage/{id}")] // HTTP GET request handler for retrieving a Page by its identifier
        public async Task<IActionResult> GetParagraphsOfPage(int id)
        {
           
            IReadOnlyCollection<Paragraph> paragraphs = await paragraphService.FindByConditionAsync(p=>p.pageId==id);
            if (paragraphs.Count == 0)
            {
                await paragraphService.CreateAsync(new Paragraph
                {
                    Page = await pageService.FindByConditionItemAsync(p => p.Id == id),
                    pageId = id,
                    ParagraphText = "",
                    ParagraphName = ""

                });
                paragraphs = await paragraphService.FindByConditionAsync(p => p.pageId == id);

            }
                return new JsonResult(paragraphs);
          
           
        }

        [HttpPost] // HTTP POST request handler for creating a new Page
        public async Task<IActionResult> Post([FromBody] PostPage Page)
        {
            if (Page == null)
            {
                return BadRequest();
            }

            var unit = await unitService.FindByConditionItemAsync(u => u.Id == Page.unitId);

            if (unit == null)
            {
                return NotFound("Unit not found");
            }

            Page newPage = new Page
            {
                Unit = unit,
                unitId = Page.unitId,

                PageName = Page.PageName,
                PhotoLinks = Page.PhotoLinks,
                VideoLinks = Page.VideoLinks,
                DateOfCreate = DateTime.Today.ToString()
            };


            var result = await pageService.CreateAsync(newPage, unit.Id);


            if (!result.IsError)
            {
                return Ok(); // Return a status of 201 Created
            }


            return BadRequest(result.Message);
        }

        [HttpGet("{userid}/{pageId}")] // HTTP GET request handler for retrieving a test by its identifier
        public async Task<IActionResult> Check(int pageId, string userId)
        {

            var page = await pageService.FindByConditionItemAsync(p => p.Id == pageId);
            if (page == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await pageService.CheckUser(page, user);
            if (result.IsError == false)
            {
                return Created($"api/tests/{page.Id}", page); // Return a status of 201 Created
            }
            return new JsonResult(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing Page
        public async Task<IActionResult> Put(int id, [FromBody] Page page)
        {
            if (page == null)
            {
                return BadRequest();
            }

            await pageService.EditAsync(id, page);
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a Page by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await pageService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
