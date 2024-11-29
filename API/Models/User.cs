using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    public ICollection<Post> Posts { get; set; }
    public string ImageUrl { get; set; } = "https://firebasestorage.googleapis.com/v0/b/toeicstudynetwork.appspot.com/o/userAvatar%2Ficon-image.png?alt=media&token=46696470-86d6-4a6a-bbd9-8a05c84841f9";
}