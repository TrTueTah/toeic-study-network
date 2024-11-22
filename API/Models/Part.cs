using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Part
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int PartNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public List<Question> Questions { get; set; }
    }
}