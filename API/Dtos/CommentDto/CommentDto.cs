namespace API.Dtos.CommentDto;

public class CommentDto
{
    
    public int Id { get; set; }
    public string UserId { get; set; }
    public int PostId { get; set; }
    public ICollection<string> MediaUrls { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}