using System.Diagnostics.CodeAnalysis;

namespace API.Models;

public class Comment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string PostId { get; set; }
    [AllowNull]
    public List<string>? MediaUrls { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}