namespace API.Models;

public class UserResult
{
    public string UserResultId { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string? ExamId { get; set; }
    public int Score { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public int TimeTaken { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Type { get; set; }
    public List<DetailResult> DetailResults { get; set; }
}
