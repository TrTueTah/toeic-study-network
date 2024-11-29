using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public ICollection<string> MediaUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<LikeModel> Likes { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TotalLikes => Likes?.Count ?? 0;
        public string UserImageUrl { get; set; }
    }
}