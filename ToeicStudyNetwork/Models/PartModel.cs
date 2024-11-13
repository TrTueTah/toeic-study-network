using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class PartModel
    {
        public string Id { get; set; }
        public int PartNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ExamId { get; set; }
    }
}