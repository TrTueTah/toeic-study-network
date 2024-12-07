using API.Dtos.ExamSeriesDto;
using API.Models;

namespace API.Dtos.ExamDto;

public class GetExamByPartDto
{
    public string ExamId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<int> PartNumbers { get; set; }
    public List<QuestionGroup> QuestionGroups { get; set; }
    public GetExamSeriesDto ExamSeries { get; set; }
}