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
                    resultId = result.Id,
                    userId = result.UserId,
                    examId = result.ExamId,
                    score = result.Score,
                    readingScore = result.ReadingScore,
                    listeningScore = result.ListeningScore,
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
            var questionCounts = new Dictionary<string, int>
            {
                { "Full Test", 200 },
                { "Part 1", 6 },
                { "Part 2", 25 },
                { "Part 3", 39 },
                { "Part 4", 30 },
                { "Part 5", 30 },
                { "Part 6", 16 },
                { "Part 7", 54 },
            };

            foreach (var dto in resultDto)
            {
                var exam = _examRepository.GetExamById(dto.ExamId);
                dto.ExamName = exam.Title;
                var types = dto.Type.Split(",");
                int totalQuestions = 0;

                foreach (var type in types)
                {
                    var trimmedType = type.Trim();
                    if (questionCounts.ContainsKey(trimmedType))
                    {
                        totalQuestions += questionCounts[trimmedType];
                        
                    }
                }

                dto.TotalQuestions = totalQuestions;
            }

            return Ok(resultDto);

        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet("getQuestionDetailResult/{detailResultId}")]
    [ProducesResponseType(typeof(QuestionDetailResultDto), 200)]

    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<QuestionDetailResultDto> GetQuestionDetailResult(string detailResultId)
    {
        try
        {
            var result = _userResultRepository.GetQuestionDetailResult(detailResultId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("getAnalysisUserResult/{userId}/{timeRange}")]
    [ProducesResponseType(typeof(AnalysisUserResultDto), 200)]

    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<AnalysisUserResultDto> GetAnalysisUserResult(string userId, string timeRange)
    {
        try
        {
            var result = _userResultRepository.GetAnalysisUserResult(userId, timeRange);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}