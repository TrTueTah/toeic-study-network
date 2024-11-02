using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public ICollection<Comment> GetAllComments()
    {
        return _context.Comments.OrderBy(c => c.Id).ToList();
    }

    public Comment GetCommentById(int id)
    {
        return _context.Comments.Find(id);
    }

    public Comment GetCommentByUserAndPostId(string userId, int postId)
    {
        return _context.Comments.Find(userId, postId);
    }

    public bool CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        return Save();
    }

    public bool DeleteComment(int id)
    {
        var comment = _context.Comments.Find(id);

        if (comment == null)
        {
            return false;
        }
        
        _context.Remove(comment);
        return Save();
    }

    public bool UpdateComment(Comment comment)
    {
        _context.Comments.Update(comment);
        return Save();
    }

    public bool CommentExists(int id)
    {
        return _context.Posts.Any(c => c.Id == id);
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}