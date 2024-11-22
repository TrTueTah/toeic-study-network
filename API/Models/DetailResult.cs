namespace API.Models;

public class DetailResult
{
    public string DetailResultId { get; set; } = Guid.NewGuid().ToString();
    public string? UserResultId { get; set; }
    public int QuestionNumber { get; set; }
    public string? UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public UserResult? UserResult { get; set; }
}
