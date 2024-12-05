using API.Dtos.QuestionDto;

namespace API.Dtos.ResultDto;

public class QuestionDetailResultDto
{
    public string? DetailResultId { get; set; }
    public string? ExamName { get; set; }
    public string? UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public string? Title { get; set; }
    public string? AnswerA { get; set; }
    public string? AnswerB { get; set; }
    public string? AnswerC { get; set; }
    public string? AnswerD { get; set; }
    public string? CorrectAnswer { get; set; }
    public int QuestionNumber { get; set; }
    public List<string>? ImageFilesUrl { get; set; }
    public List<string>? AudioFilesUrl { get; set; }
}