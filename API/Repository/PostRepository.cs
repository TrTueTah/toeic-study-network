using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repository;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;
    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Post> GetAllPosts()
    {
        return _context.Posts.ToList();
    }

    public Post GetPostById(string id)
    {
        return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
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
}