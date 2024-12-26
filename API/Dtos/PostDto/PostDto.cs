using API.Models;

namespace API.Dtos.PostDto;

public class PostDto
{
    public string Id { get; set; }
    public string Content { get; set; }

    public List<string> MediaUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Like> Likes { get; set; }
    public List<Comment> Comments { get; set; }
    public string Status { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserImageUrl { get; set; }
}