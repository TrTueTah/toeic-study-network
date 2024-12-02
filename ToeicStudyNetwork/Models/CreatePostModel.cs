using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class CreatePostModel
    {
        public string Content { get; set; }
        public List<IFormFile> MediaFiles { get; set; }
        public string UserId { get; set; }
    }
}