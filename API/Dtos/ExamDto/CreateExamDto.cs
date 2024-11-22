using API.Dtos.QuestionDto;

namespace API.Dtos.ExamDto;

public class CreateExamDto
{
    public string Title { get; set; }
    public List<CreateQuestionDto> Questions { get; set; }
}