namespace API.Models;

public class UserResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string? ExamId { get; set; }
    public int? Score { get; set; }
    public int? ReadingScore { get; set; }
    public int? ListeningScore { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public float TimeTaken { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Type { get; set; }
    public List<DetailResult> DetailResults { get; set; }
    public Exam? Exam { get; set; }
}
