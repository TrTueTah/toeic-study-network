namespace ToeicStudyNetwork.Models;

public class ExamSeriesModel
{
    public string Id { get; set; } 
    public string Name { get; set; }
    public List<ExamModel> Exams { get; set; }
}
