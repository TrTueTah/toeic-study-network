using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Test;

public class TestResultViewModel
{
    public string? UserResultId { get; set; }
    public string? UserId { get; set; }
    public string? ExamId { get; set; }
    public string? ExamName { get; set; }
    public int Score { get; set; }
    public int ReadingScore { get; set; }
    public int ListeningScore { get; set; }
    public int ReadingCorrectAnswerAmount { get; set; }
    public int ListeningCorrectAnswerAmount { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public int IncorrectAnswerAmount { get; set; }
    public int WithoutAnswerAmount { get; set; }
    public float TimeTaken { get; set; }

    public string TimeTakenFormatted
    {
        get
        {
            TimeSpan time = TimeSpan.FromSeconds(TimeTaken);
            return time.ToString(@"hh\:mm\:ss");
        }
    }
    public DateTime CreatedAt { get; set; }
    public string? Type { get; set; }
    public List<DetailResultModel> DetailResults { get; set; }

}
