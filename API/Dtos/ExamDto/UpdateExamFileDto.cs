namespace API.Dtos.ExamDto;

public class UpdateExamFileDto
{
    public string ExamId { get; set; }
    public IFormFile audioFile { get; set; }
}