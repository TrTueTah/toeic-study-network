using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class ExamModel
    {
        public string Id;
        public string Title;
        public DateTime CreatedAt;
        
        public string? AudioFilesUrl { get; set; }
        public List<QuestionGroupModel> QuestionGroups { get; set; }
        public ExamSeriesModel ExamSeries { get; set; }
    }
}
