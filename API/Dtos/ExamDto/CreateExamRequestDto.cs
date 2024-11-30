using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.ExamDto
{
    public class CreateExamRequestDto
    {
        public string Title { get; set; }
        public string ExamSeriesId { get; set; }
    }
}