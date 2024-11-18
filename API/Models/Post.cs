namespace API.Models;

public class Post
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Content { get; set; }
    public List<string> MediaUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Like> Likes { get; set; }
    public List<Comment> Comments { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}