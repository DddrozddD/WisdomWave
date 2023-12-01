using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BLL.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using WisdomWave.Models;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly AnswerService _answerService;
    private readonly QuestionService _questionService;
    private readonly SubQuestionService _subQuestionService;

    public AnswersController(AnswerService answerService, QuestionService questionService)
    {
        _answerService = answerService;
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnswers()
    {
        var answers = await _answerService.GetAsyncs();
        return new JsonResult(answers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnswer(int id)
    {
        var answer = await _answerService.FindByConditionItemAsync(a => a.Id == id);

        if (answer == null)
        {
            return NotFound();
        }

        return new JsonResult(answer);
    }

    [HttpPost()]

    public async Task<IActionResult> CreateAnswerInQuestion([FromBody] PushAnswer answer)
    {
   
        var question = await _questionService.FindByConditionItemAsync(a => a.Id == answer.questionId);

        if (question == null)
        {
            return NotFound("Question not found");
        }
        var result = await _answerService.CreateAsync(new Answer
        {
            AnswerText= answer.AnswerText,
            IsCorrect=answer.IsCorrect,
            questionId=question.Id,
            Question = question
        });

        if (result.IsError == false)
        {
            return Ok();
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

        var result = await _answerService.CreateAsync(answer);

        if (result.IsError == false)
        {
            return Ok();
        }

        return BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnswer(int id, [FromBody] PushAnswer answer)
    {

        var result = await _answerService.EditAsync(id, new Answer
        {
            Id = id,
            AnswerText= answer.AnswerText,
            IsCorrect= answer.IsCorrect,
            questionId = answer.questionId,
            Question = await _questionService.FindByConditionItemAsync(q=>q.Id==answer.questionId)
        });

        if (result.IsError == false)
        {
            return Ok();
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
