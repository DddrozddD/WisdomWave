using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;

namespace WisdomWave.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller
    [ApiController] // Indicate that this is an API controller
    public class ParagraphController : ControllerBase
    {
        private readonly ParagraphService paragraphService;
        private readonly UnitService unitService;

        public ParagraphController(ParagraphService paragraphService)
        {
            this.paragraphService = paragraphService;
        }

        [HttpGet] // HTTP GET request handler for retrieving all paragraphs
        public async Task<IActionResult> Get()
        {
            var paragraphs = await paragraphService.GetAsyncs();
            return Ok(paragraphs);
        }

        [HttpGet("{id}")] // HTTP GET request handler for retrieving a paragraph by its identifier
        public async Task<IActionResult> Get(int id)
        {
            var paragraph = await paragraphService.FindByConditionItemAsync(p => p.Id == id);
            if (paragraph == null)
            {
                return NotFound();
            }
            return Ok(paragraph);
        }

        [HttpPost("unitID")] // HTTP POST request handler for creating a new paragraph
        public async Task<IActionResult> Post([FromBody] Paragraph paragraph, int unitID)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }

            var unit = await unitService.FindByConditionItemAsync(u => u.Id == unitID);

            if (unit == null)
            {
                return NotFound("Unit not found");
            }

            paragraph.Unit = unit;

            var result = await paragraphService.CreateAsync(paragraph, unit.Id);
            if (!result.IsError)
            {
                return Created($"api/paragraphs/{paragraph.Id}", paragraph); // Return a status of 201 Created
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")] // HTTP PUT request handler for updating an existing paragraph
        public async Task<IActionResult> Put(int id, [FromBody] Paragraph paragraph)
        {
            if (paragraph == null)
            {
                return BadRequest();
            }

            await paragraphService.EditAsync(id, paragraph);
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
