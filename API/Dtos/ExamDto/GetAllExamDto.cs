using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.ExamSeriesDto;
using API.Dtos.QuestionDto;
using API.Dtos.QuestionGroupDto;
using API.Models;

namespace API.Dtos.ExamDto
{
    public class GetAllExamDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? AudioFilesUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetAllQuestionGroupDto> QuestionGroups { get; set; }
        public GetExamSeriesDto ExamSeries { get; set; }
    }
}