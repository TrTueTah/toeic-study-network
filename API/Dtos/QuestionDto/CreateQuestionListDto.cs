namespace API.Dtos.QuestionDto;

public class CreateQuestionListDto
{
    public string ExamId { get; set; }
    public List<CreateQuestionDto> Questions { get; set; } = new();
}