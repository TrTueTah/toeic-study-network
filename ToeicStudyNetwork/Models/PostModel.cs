using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ICollection<string> MediaUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<LikeModel> Likes { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public AppUserModel User { get; set; }
        public int TotalLikes => Likes?.Count ?? 0;
    }
}