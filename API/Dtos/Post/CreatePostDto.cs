namespace API.Dtos.Post;

public class CreatePostDto
{
    public string Content { get; set; }
    public List<string> MediaUrls { get; set; } = new List<string>(); // Khởi tạo một danh sách rỗng
    public string UserId { get; set; }
}