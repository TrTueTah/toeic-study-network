using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Dtos.RequestDtos
{
    public class UpdateUserRequest
    {
        [AllowNull]
        public string Username { get; set; }
        [AllowNull]
        public IFormFile ImageFile { get; set; }
    }
}