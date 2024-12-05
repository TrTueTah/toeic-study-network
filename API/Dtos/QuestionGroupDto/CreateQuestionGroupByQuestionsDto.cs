using API.Models;

namespace API.Dtos.QuestionGroupDto;

public class CreateQuestionGroupByQuestionsDto
{
    public int PartNumber { get; set; }
    public List<Question> Questions { get; set; } = new();
}