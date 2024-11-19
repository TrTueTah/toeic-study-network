using System.Collections;

namespace API.Dtos.CommentDto;

public class CreateCommentDto
{
    public string UserId { get; set; }
    public string PostId { get; set; }
    public List<IFormFile> MediaFiles { get; set; }
    public string Content { get; set; }
}