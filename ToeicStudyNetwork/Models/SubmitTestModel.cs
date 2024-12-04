namespace ToeicStudyNetwork.Models;

public class SubmitTestModel
{
    public string UserId { get; set; }
    public string ExamId { get; set; }
    public int TimeTaken { get; set; }
    public string Type { get; set; }
    public Dictionary<int, string> Answers { get; set; }
}

