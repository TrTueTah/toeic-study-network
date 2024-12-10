using API.Models;

namespace API.Interfaces;

public interface IPostRepository
{
    List<Post> GetAllPosts(int page, int limit);
    List<Post> GetLikedPostsByUserId(string userId, int page, int limit);
    Post GetPostById(string id);
    List<Post> GetPostsByUserId(string id);
    bool CreatePost(Post post);
    bool UpdatePost(Post post);
    bool DeletePost(string id);
    bool Save();
    bool PostExists(string id);
    int GetAllPostsCount();
    int GetLikedPostsByUserIdCount(string userId);
}