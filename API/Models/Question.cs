using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? AnswerA { get; set; } = "";
        public string? AnswerB { get; set; } = "";
        public string? AnswerC { get; set; } = "";
        [AllowNull]
        public string? AnswerD { get; set; }
        public string CorrectAnswer { get; set; } = "";
        public int QuestionNumber { get; set; }
        public int PartNumber { get; set; }
        public string GroupId { get; set; }
        public QuestionGroup Group { get; set; }
    }
}