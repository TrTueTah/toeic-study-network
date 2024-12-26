using API.Models;

namespace API.Dtos.QuestionGroupDto;

public class GetAllQuestionGroupDto
{
    public string Id { get; set; }
    public int PartNumber { get; set; }
    
    public List<string> ImageFilesUrl { get; set; } = new();
    public List<string> AudioFilesUrl { get; set; } = new();
    
    public List<Question> Questions { get; set; } = new();
}