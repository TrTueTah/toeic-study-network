namespace API.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public List<string> MediaUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}