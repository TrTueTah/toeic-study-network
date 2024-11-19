using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.QuestionDto
{
    public class UploadReadingDto
    {
        public IFormFile QuestionFile { get; set; }
        public IFormFile AnswerFile { get; set; }
    }
}