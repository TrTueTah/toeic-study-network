namespace API.Models;

public class Like
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string PostId { get; set; }
    public DateTime LikedAt { get; set; }
}