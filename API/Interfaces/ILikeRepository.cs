using System.Collections;
using API.Models;

namespace API.Interfaces;

public interface ILikeRepository
{
    bool AddLike(Like like);
    bool RemoveLike(int postId, string userId);
    ICollection<Like> GetLikesByPostId(int postId);
    bool UserHasLikedPost(int postId, string userId);
}
