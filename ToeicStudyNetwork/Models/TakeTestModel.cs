using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToeicStudyNetwork.Models
{
    public class TakeTestModel
    {
        public string Title { get; set; }
        public List<PartModel> PartModels { get; set; }
        public DateTime TimeLimit { get; set; } = DateTime.Now.AddMinutes(120);

    }
}