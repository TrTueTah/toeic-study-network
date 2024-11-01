using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public AppUser GetUserById(string userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
    }

    public bool UserExists(string userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}