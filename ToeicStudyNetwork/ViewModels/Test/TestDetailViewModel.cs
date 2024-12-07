using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.ViewModels.Test
{
    public class TestDetailViewModel
    {
        public ExamModel Exam { get; set; }
        public List<UserResultViewModel>? UserResults { get; set; }
    }
}
