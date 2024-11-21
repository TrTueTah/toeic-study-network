using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.PartDto
{
    public class UploadMediaFileDto
    {
        [AllowNull]
        public List<IFormFile>? ImageFiles { get; set; } = new List<IFormFile>();
        [AllowNull]
        public List<IFormFile>? AudioFiles { get; set; } = new List<IFormFile>();
    }
}