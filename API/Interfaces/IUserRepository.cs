using API.Models;

namespace API.Interfaces;

public interface IUserRepository
{
    User GetUserById(string userId);
    string GetUserNameById(string userId);
    User GetUserByUsername(string username);
    bool UserExists(string userId);
    User GetUserByEmail(string email);
    User CreateUser(User user);
    string HashPassword(string password);
}