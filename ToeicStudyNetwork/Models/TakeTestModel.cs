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
        public Dictionary<int, List<QuestionModel>> PartQuestions { get; set; }
        
        public DateTime TimeLimit { get; set; } = DateTime.Now.AddMinutes(120);
    }
}
