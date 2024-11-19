using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.QuestionDto
{
    public class UploadQuestionDto
    {
        public List<string> PartIds { get; set; }
        public IFormFile File { get; set; }
    }
}