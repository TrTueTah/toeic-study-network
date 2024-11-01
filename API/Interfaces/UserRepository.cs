using API.Models;

namespace API.Interfaces;

public interface IUserRepository
{
    AppUser GetUserById(string userId);
    bool UserExists(string userId);
}