using API.Dtos.ResultDto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/result")]
public class UserResultController : ControllerBase
{
    private readonly IUserResultRepository _userResultRepository;
    private readonly IMapper _mapper;
    private readonly IExamRepository _examRepository;

    public UserResultController(IUserResultRepository userResultRepository, IMapper mapper, IExamRepository examRepository)
    {
        _userResultRepository = userResultRepository;
        _mapper = mapper;
        _examRepository = examRepository;
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
                    type = result.Type,
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

    [HttpGet("getUserResultByUserId/{userId}")]
    [ProducesResponseType(typeof(List<GetAllUserResultDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<List<GetAllUserResultDto>> GetUserResultByUserId(string userId)
    {
        try
        {
            var result = _userResultRepository.GetAllUserResultsByUserId(userId);
            var resultDto = _mapper.Map<List<GetAllUserResultDto>>(result);
            foreach (var dto in resultDto)
            {
                var exam = _examRepository.GetExamById(dto.ExamId);
                dto.ExamName = exam.Title;
            }
            return Ok(resultDto);

        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}