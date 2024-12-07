using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Dtos;

public class TestPracticeResponse
{
    public string ExamId { get; set; }
    public string Title { get; set; }
    public List<int> PartNumbers { get; set; } = new();
    public List<QuestionGroupModel> QuestionGroups { get; set; }
}
