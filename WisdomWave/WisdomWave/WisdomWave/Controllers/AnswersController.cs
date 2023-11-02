using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly AnswerService _answerService;
    private readonly QuestionService _questionService;
    private readonly SubQuestionService _subQuestionService;

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

    [HttpPost("questionId")]
    public async Task<IActionResult> CreateAnswerInQuestion([FromBody] Answer answer, int questionId)
    {
   
        var question = await _questionService.FindByConditionItemAsync(a => a.Id == questionId);

        if (question == null)
        {
            return NotFound("Question not found");
        }

        answer.Question = question;

        var result = await _answerService.CreateAsync(answer,question.Id);

        if (result.IsError == false)
        {
            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        return BadRequest(result.Message);
    }

    [HttpPost("subQuestionId")]
    public async Task<IActionResult> CreateAnswerInSubQuestion([FromBody] Answer answer, int subQuestionId)
    {

        var subquestion = await _subQuestionService.FindByConditionItemAsync(a => a.Id == subQuestionId);

        if (subquestion != null)
        {
            return NotFound("SubQuestion not found");
        }
        answer.SubQuestion = subquestion;

        var result = await _answerService.CreateAsync(answer, subquestion.Id);

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
