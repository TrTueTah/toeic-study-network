namespace API.Dtos.ResultDto;

public class DetailResultDto
{
    public int QuestionNumber { get; set; }
    public string? UserAnswer { get; set; }
    public string? CorrectAnswer { get; set; }
    public bool IsCorrect { get; set; }
}
