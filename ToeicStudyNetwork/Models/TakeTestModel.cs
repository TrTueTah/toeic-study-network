using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class TakeTestModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string TestType { get; set; }
        
        public string? AudioFilesUrl { get; set; }
        public Dictionary<int, List<QuestionGroupModel>> PartQuestions { get; set; }
        
        public TimeSpan TimeLimit { get; set; } = new TimeSpan(2, 0, 0);
    }
}
