using API.Dtos.ResultDto;
using API.Models;

namespace API.Interfaces;

public interface IUserResultRepository
{
    UserResult CalculateAndSaveResult(SubmitResultDto submission);
    Task<UserResultDto> GetDetailsResultAsync(string userResultId);
    List<UserResult> GetAllUserResultsByUserId(string userId);
    QuestionDetailResultDto GetQuestionDetailResult(string detailResultId);
    AnalysisUserResultDto GetAnalysisUserResult(string userId, string timeRange);
    List<UserResult> GetAllUserResultsByExamId(string userId, string examId);
}