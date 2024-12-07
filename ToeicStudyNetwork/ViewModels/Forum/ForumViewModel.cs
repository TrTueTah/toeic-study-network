using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Forum
{
    public class ForumViewModel
    {
        public List<PostModel> Posts { get; set; }
        public UserModel User { get; set; }
        public string Type { get; set; }
    }
}