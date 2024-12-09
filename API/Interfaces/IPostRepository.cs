using API.Models;

namespace API.Interfaces;

public interface IPostRepository
{
    List<Post> GetAllPosts(int page, int limit);
    Post GetPostById(string id, int page, int limit);
    List<Post> GetPostsByUserId(string id);
    bool CreatePost(Post post);
    bool UpdatePost(Post post);
    bool DeletePost(string id);
    bool Save();
    bool PostExists(string id);
    int GetAllPostsCount();
    int GetCommentCountByPostId(string id);
}