using API.Models;

namespace API.Interfaces;

public interface IUserRepository
{
    AppUser GetUserById(string userId);
    string GetUserNameById(string userId);
    bool UserExists(string userId);
}