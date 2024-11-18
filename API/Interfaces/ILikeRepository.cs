using System.Collections;
using API.Models;

namespace API.Interfaces;

public interface ILikeRepository
{
    bool CreateLike(Like like);
    bool DeleteLike(string postId, string userId);
    List<Like> GetLikesByPostId(string postId);
    bool UserHasLikedPost(string postId, string userId);
}
