using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.LikeDto
{
    public class LikeResponseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string Username { get; set; }
        public DateTime LikedAt { get; set; }
    }
}