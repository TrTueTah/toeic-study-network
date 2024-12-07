namespace ToeicStudyNetwork.ViewModels.Test;

public class UserResultViewModel
{
    public string UserResultId { get; set; }
    public string? UserId { get; set; }
    public string? ExamId { get; set; }
    public string? ExamName { get; set; }
    public int Score { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public float TimeTaken { get; set; }
    public DateTime CreateAt { get; set; }
    
    public int TotalQuestions { get; set; }
    public string? Type { get; set; }
}
