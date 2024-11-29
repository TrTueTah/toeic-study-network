using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class ForumModel
    {
        public List<PostModel> Posts { get; set; }
        public UserModel User { get; set; }
    }
}