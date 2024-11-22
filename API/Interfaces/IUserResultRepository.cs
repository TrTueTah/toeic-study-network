using API.Dtos.ResultDto;
using API.Models;

namespace API.Interfaces;

public interface IUserResultRepository
{
    UserResult CalculateAndSaveResult(SubmitResultDto submission);
}