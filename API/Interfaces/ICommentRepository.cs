using API.Models;

namespace API.Interfaces;

public interface ICommentRepository
{
    List<Comment> GetAllComments();
    Comment GetCommentById(string id);
    Comment GetCommentByUserAndPostId(string userId, string postId);
    bool CreateComment(Comment comment);
    bool DeleteComment(string id);
    bool UpdateComment(Comment comment);
    
    bool CommentExists(string id);
}