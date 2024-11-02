using API.Models;

namespace API.Interfaces;

public interface ICommentRepository
{
    ICollection<Comment> GetAllComments();
    Comment GetCommentById(int id);
    Comment GetCommentByUserAndPostId(string userId, int postId);
    bool CreateComment(Comment comment);
    bool DeleteComment(int id);
    bool UpdateComment(Comment comment);
    
    bool CommentExists(int id);
}