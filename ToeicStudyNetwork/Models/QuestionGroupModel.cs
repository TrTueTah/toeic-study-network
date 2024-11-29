namespace ToeicStudyNetwork.Models;

public class QuestionGroupModel
{
    public string Id { get; set; }
    public string ExamId { get; set; }
    public ExamModel Exam { get; set; }
    public int PartNumber { get; set; }
    
    public List<string> ImageFilesUrl { get; set; } 
    public List<string> AudioFilesUrl { get; set; } 
    
    public List<QuestionModel> Questions { get; set; }
}
