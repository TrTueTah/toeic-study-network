using System.Diagnostics.CodeAnalysis;

namespace API.Dtos.CommentDto;

public class CommentDto
{

    public string Id { get; set; }
    public string UserId { get; set; }
    public string PostId { get; set; }
    [AllowNull]
    public List<string>? MediaUrls { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}