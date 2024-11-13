using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.PartDto
{
    public class PartResponseDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int PartNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public string ExamId { get; set; }
    }
}