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
}