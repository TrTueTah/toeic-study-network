using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Test;

public class TestPracticeViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string TestType { get; set; }
    public List<int> PartNumbers { get; set; }
    public Dictionary<int, List<QuestionGroupModel>> PartQuestions { get; set; }
    public TimeSpan TimeLimit { get; set; } = new TimeSpan(2, 0, 0);
}
