namespace API.Models;

public class ExamSeries
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Exam> Exams { get; set; } = new();
}