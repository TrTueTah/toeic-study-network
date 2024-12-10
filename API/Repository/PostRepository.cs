using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;
    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Post> GetAllPosts(int page, int limit)
    {
        return _context.Posts
            .Include(p=>p.Comments)
            .Include(p=>p.Likes)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToList();
    }

    public Post GetPostById(string id)
    {
        return _context.Posts
            .Where(p => p.Id == id)
            .Include(p=>p.Comments)
            .Include(p=>p.Likes)
            .FirstOrDefault() ?? throw new InvalidOperationException();
    }

    public List<Post> GetPostsByUserId(string id)
    {
        return _context.Posts.Where(p => p.UserId == id).ToList();
    }

    public bool CreatePost(Post post)
    {
        _context.Posts.Add(post);
        return Save();
    }

    public bool UpdatePost(Post post)
    {
        _context.Posts.Update(post);
        return Save();
    }

    public bool DeletePost(string id)
    {
        var post = _context.Posts.Find(id);

        if (post == null)
        {
            return false;
        }

        _context.Posts.Remove(post);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool PostExists(string id)
    {
        return _context.Posts.Any(c => c.Id == id);
    }

    public int GetAllPostsCount()
    {
        return _context.Posts.Count();
    }
}