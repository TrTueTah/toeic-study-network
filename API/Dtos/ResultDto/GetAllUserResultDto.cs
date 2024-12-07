namespace API.Dtos.ResultDto;

public class GetAllUserResultDto
{
    public string UserResultId { get; set; }
    public string ExamId { get; set; }
    public string ExamName { get; set; }
    public int TotalQuestions { get; set; }
    public int Score { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public float TimeTaken { get; set; }
    public string Type { get; set; }
    public DateTime CreateAt { get; set; }
}