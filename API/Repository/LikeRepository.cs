using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repository;

public class LikeRepository : ILikeRepository
{
    private readonly ApplicationDbContext _context;

    public LikeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool CreateLike(Like like)
    { 
        if (UserHasLikedPost(like.PostId, like.UserId))
        {
            return false;
        }
        _context.Likes.Add(like);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteLike(string postId, string userId)
    {
        var like = _context.Likes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);
        if (like == null)
        {
            return false;
        }

        _context.Likes.Remove(like);
        _context.SaveChanges();
        return true;
    }

    public List<Like> GetLikesByPostId(string postId)
    {
        return _context.Likes.Where(l => l.PostId == postId).ToList();
    }

    public bool UserHasLikedPost(string postId, string userId)
    {
        return _context.Likes.Any(l => l.PostId == postId && l.UserId == userId);
    }
}
