using System.Collections;

namespace API.Dtos.CommentDto;

public class CreateCommentDto
{
    public string UserId { get; set; }
    public int PostId { get; set; }
    public ICollection<IFormFile> MediaFiles { get; set; }
    public string Content { get; set; }
}