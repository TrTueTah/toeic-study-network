using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public User GetUserById(string userId)
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

    public string GetUserNameById(string userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId).Username;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public User CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public string GetUserImageUrlById(string userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId).ImageUrl;
    }

    public string GetUserIdByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email).Id;
    }

    public User UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }
}