using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class PersonalModel
    {
        public UserModel User { get; set; }
        public List<UserResultModel> UserResults { get; set; }
    }
}