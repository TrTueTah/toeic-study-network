using API.Dtos.ResultDto;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/result")]
public class UserResultController : ControllerBase
{
    private readonly IUserResultRepository _userResultRepository;

    public UserResultController(IUserResultRepository userResultRepository)
    {
        _userResultRepository = userResultRepository;
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(UserResult), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult SubmitResult([FromBody] SubmitResultDto submission)
    {
        try
        {
            var result = _userResultRepository.CalculateAndSaveResult(submission);

            return Ok(new
            {
                message = "Result submitted successfully.",
                result = new
                {
                    resultId = result.UserResultId,
                    userId = result.UserId,
                    examId = result.ExamId,
                    score = result.Score,
                    correctAnswerAmount = result.CorrectAnswerAmount,
                    timeTaken = result.TimeTaken,
                    createdAt = result.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet("getUserResult/{userResultId}")]
    public async Task<ActionResult<UserResultDto>> GetDetailsResult(string userResultId)
    {
        try
        {
            var result = await _userResultRepository.GetDetailsResultAsync(userResultId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}