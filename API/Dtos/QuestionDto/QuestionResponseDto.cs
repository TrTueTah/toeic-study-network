using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.QuestionDto
{
    public class QuestionResponseDto
    {
        public string? Title { get; set; }
        public string? AnswerA { get; set; } = "";
        public string? AnswerB { get; set; } = "";
        public string? AnswerC { get; set; } = "";
        public string? AnswerD { get; set; } = "";
        public string CorrectAnswer { get; set; }
        public int QuestionNumber { get; set; }
        public string PartId { get; set; }
        public int PartNumber { get; set; }
    }
}