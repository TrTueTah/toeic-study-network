using System.Collections;

namespace API.Dtos.PostDto;

public class CreatePostDto
{
    public string Content { get; set; }
    public ICollection<IFormFile> MediaFiles { get; set; }
    public string UserId { get; set; }
}