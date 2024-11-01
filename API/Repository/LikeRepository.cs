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

    public bool AddLike(Like like)
    { 
        if (UserHasLikedPost(like.PostId, like.UserId))
        {
            return false;
        }
        _context.Likes.Add(like);
        return Save();
    }

    public bool RemoveLike(int postId, string userId)
    {
        var like = _context.Likes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);
        if (like == null)
        {
            return false;
        }

        _context.Likes.Remove(like);
        return Save();
    }

    public ICollection<Like> GetLikesByPostId(int postId)
    {
        return _context.Likes.Where(l => l.PostId == postId).ToList();
    }

    public bool UserHasLikedPost(int postId, string userId)
    {
        return _context.Likes.Any(l => l.PostId == postId && l.UserId == userId);
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
