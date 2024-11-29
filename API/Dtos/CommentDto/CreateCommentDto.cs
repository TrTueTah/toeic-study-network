using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace API.Dtos.CommentDto;

public class CreateCommentDto
{
    public string UserId { get; set; }
    public string PostId { get; set; }
    [AllowNull]
    public List<IFormFile>? MediaFiles { get; set; }
    public string Content { get; set; }
}