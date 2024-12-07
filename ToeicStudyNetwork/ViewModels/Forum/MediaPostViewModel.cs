using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Forum
{
    public class MediaPostViewModel
    {
        public PostModel Post { get; set; }
        public UserModel User { get; set; }
    }
}