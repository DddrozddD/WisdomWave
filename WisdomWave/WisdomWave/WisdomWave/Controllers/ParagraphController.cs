using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using WisdomWave.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class ParagraphController : ControllerBase
    {
        private readonly ParagraphService paragraphService;
        private readonly PageService pageService;
        private readonly UnitService unitService;
        private readonly UserManager<WwUser> _userManager;

        public ParagraphController(ParagraphService paragraphService, UnitService unitService, PageService pageService)
        {
            this.unitService = unitService;
            this.paragraphService = paragraphService;
            this.pageService = pageService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all paragraphs
        public async Task<IActionResult> Get()
        {
            var paragraphs = await paragraphService.GetAsyncs();
            return new JsonResult(paragraphs);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a paragraph by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var paragraph = await paragraphService.FindByConditionItemAsync(p => p.Id == id);
            if (paragraph == null)
            {
                return NotFound();
            }
            return new JsonResult(paragraph);
        }


        [HttpPost] // HTTP POST request handler for creating a new Page
        public async Task<IActionResult> Post([FromBody] PostParagraph paragraph)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }



            var result = await paragraphService.CreateAsync(new Paragraph
            {
                ParagraphName = paragraph.ParagraphName,
                ParagraphText= paragraph.ParagraphText,
                pageId =paragraph.pageId,
                Page = await pageService.FindByConditionItemAsync(p=>p.Id==paragraph.pageId)
            });
            if (!result.IsError)
            {
                return Ok(); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }


        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing paragraph
        public async Task<IActionResult> Put(int id, [FromBody] PostParagraph paragraph)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }



            await paragraphService.EditAsync(id, new Paragraph
            {
                Id= id,
                ParagraphName = paragraph.ParagraphName,
                ParagraphText = paragraph.ParagraphText,
                pageId = paragraph.pageId,
                Page = await pageService.FindByConditionItemAsync(p => p.Id == paragraph.pageId)
            });
            return NoContent(); // Return a status of 204 No Content
        }

        [HttpDelete("{id}")] // HTTP DELETE request handler for deleting a paragraph by its identifier
        public async Task<IActionResult> Delete(int id)
        {
            await paragraphService.DeleteAsync(id);
            return NoContent(); // Return a status of 204 No Content
        }
    }
}
