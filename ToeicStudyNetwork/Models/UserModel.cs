using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ToeicStudyNetwork.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}
