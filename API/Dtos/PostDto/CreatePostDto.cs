namespace API.Dtos.PostDto;

public class CreatePostDto
{
    public string Content { get; set; }
    public List<IFormFile> MediaFiles { get; set; }
    public string UserId { get; set; }
}