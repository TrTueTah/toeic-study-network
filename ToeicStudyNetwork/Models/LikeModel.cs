using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class LikeModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public DateTime LikedAt { get; set; }
        public string Username { get; set; }
    }
}