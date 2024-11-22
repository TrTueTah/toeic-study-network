namespace API.Dtos.ResultDto;

public class SubmitResultDto
{
    public string UserId { get; set; }
    public string ExamId { get; set; }
    public int TimeTaken { get; set; }
    public Dictionary<int, string> Answers { get; set; }
}