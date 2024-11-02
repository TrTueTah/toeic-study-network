using System.Collections;
using API.Models;

namespace API.Interfaces;

public interface ILikeRepository
{
    bool CreateLike(Like like);
    bool DeleteLike(int postId, string userId);
    ICollection<Like> GetLikesByPostId(int postId);
    bool UserHasLikedPost(int postId, string userId);
}
