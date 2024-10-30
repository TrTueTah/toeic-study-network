using API.Models;

namespace API.Interfaces;

public interface IPostRepository
{
    ICollection<Post> GetAllPosts(); 
    Post GetPostByPostId(int id);
    ICollection<Post> GetPostsByUserId(string id);
    bool CreatePost(Post post);
    bool UpdatePost(Post post);
    bool DeletePost(int id);
    bool Save();
    bool PostExists(int id);
}