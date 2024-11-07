using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class LikeModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public DateTime LikedAt { get; set; }
    }
}