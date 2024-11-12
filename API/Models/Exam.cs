using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Exam
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public List<Part> Parts { get; set; } = new List<Part>();
    }
}