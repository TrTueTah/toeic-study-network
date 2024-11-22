using System.Diagnostics.CodeAnalysis;

namespace API.Models;

public class QuestionGroup
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ExamId { get; set; }
    public Exam Exam { get; set; }
    
    public List<string> ImageFilesUrl { get; set; } = new();
    public List<string> AudioFilesUrl { get; set; } = new();
    
    public List<Question> Questions { get; set; } = new();
}