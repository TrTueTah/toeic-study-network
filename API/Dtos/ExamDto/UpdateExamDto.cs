namespace API.Dtos.ExamDto;

public class UpdateExamDto
{
    public string ExamId { get; set; }
    public IFormFile audioFile { get; set; }
}