using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly AnswerService _answerService;

    public AnswersController(AnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnswers()
    {
        var answers = await _answerService.GetAsyncs();
        return Ok(answers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnswer(int id)
    {
        var answer = await _answerService.FindByConditionItemAsync(a => a.Id == id);

        if (answer == null)
        {
            return NotFound();
        }

        return Ok(answer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnswer([FromBody] Answer answer)
    {
        var result = await _answerService.CreateAsync(answer);

        if (result.IsError == false)
        {
            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        return BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnswer(int id, [FromBody] Answer answer)
    {
        if (id != answer.Id)
        {
            return BadRequest();
        }

        var result = await _answerService.EditAsync(id, answer);

        if (result.IsError == false)
        {
            return NoContent();
        }

        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        await _answerService.DeleteAsync(id);
        return NoContent();
    }
}
