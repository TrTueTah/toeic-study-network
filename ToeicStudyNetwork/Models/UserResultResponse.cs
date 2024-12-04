namespace ToeicStudyNetwork.Models;

public class UserResultResponse
{
    public string? UserResultId { get; set; }
    public string? UserId { get; set; }
    public string? ExamId { get; set; }
    public int Score { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public float TimeTaken { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Type { get; set; }
}
