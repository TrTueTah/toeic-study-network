using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.ExamSeriesDto;
using API.Dtos.QuestionDto;
using API.Models;

namespace API.Dtos.ExamDto
{
    public class GetAllExamDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string? AudioFilesUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public List<QuestionGroup> QuestionGroups { get; set; } = new();
        public GetExamSeriesDto ExamSeries { get; set; }
    }
}