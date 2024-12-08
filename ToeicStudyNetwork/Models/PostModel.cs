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
        public List<string> MediaUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LikeModel> Likes { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
    }
}
