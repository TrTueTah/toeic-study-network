using API.Models;

namespace API.Interfaces;

public interface IUserRepository
{
    string GetUserImageUrlById(string userId);
    User GetUserById(string userId);
    string GetUserNameById(string userId);
    User GetUserByUsername(string username);
    bool UserExists(string userId);
    Task<User> GetUserByEmail(string email);
    User CreateUser(User user);
    string HashPassword(string password);
    string GetUserIdByEmail(string email);
    User UpdateUser(User user);
}