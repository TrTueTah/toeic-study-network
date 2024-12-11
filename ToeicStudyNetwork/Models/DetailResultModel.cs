namespace ToeicStudyNetwork.Models;

public class DetailResultModel
{
    public string Id { get; set; }
    public string? UserResultId { get; set; }
    public int QuestionNumber { get; set; }
    public string? UserAnswer { get; set; }
    public string? CorrectAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public UserResultModel? UserResult { get; set; }
}
