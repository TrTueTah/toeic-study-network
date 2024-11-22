namespace API.Models
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public List<QuestionGroup> QuestionGroups { get; set; } = new();
    }
}