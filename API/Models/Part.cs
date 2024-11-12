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
        [AllowNull]
        public string? ImageFile { get; set; } = "";
        [AllowNull]
        public string? AudioFile { get; set; } = "";
        [ForeignKey("Exam")]
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}