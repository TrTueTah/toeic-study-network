using API.Dtos.QuestionDto;

namespace API.Dtos.QuestionGroupDto;

public class CreateQuestionGroupDto
{
    public int PartNumber { get; set; }
    public string ExamId { get; set; }
    public List<IFormFile> ImageFiles { get; set; } = new();
    public List<IFormFile> AudioFiles { get; set; } = new();
    public List<CreateQuestionDto> Questions { get; set; } = new();
}