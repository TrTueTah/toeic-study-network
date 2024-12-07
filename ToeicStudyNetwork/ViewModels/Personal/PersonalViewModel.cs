using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Personal
{
    public class PersonalViewModel
    {
        public UserModel User { get; set; }
        public List<UserResultModel> UserResults { get; set; }
    }
}