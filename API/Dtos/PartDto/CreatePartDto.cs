using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.PartDto
{
    public class CreatePartDto
    {
        public int PartNumber { get; set; }
        [AllowNull]
        public string? ImageFile { get; set; } = "";
        [AllowNull]
        public string? AudioFile { get; set; } = "";
        public string ExamId { get; set; }
    }
}