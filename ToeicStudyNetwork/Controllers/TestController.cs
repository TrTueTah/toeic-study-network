using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Controllers;

[Route("[controller]")]
public class TestController : Controller
{
    private readonly HttpClient _httpClient;
    public TestController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var exams = await FetchExamsAsync();

        var examSeries = exams
            .GroupBy(e => e.ExamSeries.Id)
            .Select(group => new ExamSeriesModel()
            {
                Id = group.Key,
                Name = group.First().ExamSeries.Name,
                Exams = group.ToList()
            })
            .ToList();

        return View(examSeries);
    }
    
    [NonAction]
    private async Task<List<ExamModel>> FetchExamsAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/exam/getAllExams");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var exams = JsonConvert.DeserializeObject<List<ExamModel>>(responseString);

        return exams;
    }
    
    [NonAction]
    private async Task<ExamModel> FetchExamAsync(string id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/exam/getExamById/{id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var exam = JsonConvert.DeserializeObject<ExamModel>(responseString);

        return exam;
    }
    
    [NonAction]
    public async Task<List<QuestionModel>> FetchQuestionsAsync(string id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/question/getQuestionsByPartId/{id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var questions = JsonConvert.DeserializeObject<List<QuestionModel>>(responseString);

        return questions;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DetailExam(string id)
    {
        var exam = await FetchExamAsync(id);
        return View("Detail", exam);
    }

    [HttpGet("{id}/start")]
    public async Task<IActionResult> StartExam(string id)
    {
        var exam = await FetchExamAsync(id);
    
        TakeTestModel takeTestModel = new()
        {
            Id = exam.Id,
            TestType = "Full Test",
            Title = exam.Title,
            AudioFilesUrl = exam.AudioFilesUrl,
            PartQuestions = new Dictionary<int, List<QuestionGroupModel>>(),
            TimeLimit = new TimeSpan(2, 0, 0)
        };

        foreach (var questionGroup in exam.QuestionGroups)
        {
            if (!takeTestModel.PartQuestions.ContainsKey(questionGroup.PartNumber))
            {
                takeTestModel.PartQuestions[questionGroup.PartNumber] = new List<QuestionGroupModel>();
            }

            takeTestModel.PartQuestions[questionGroup.PartNumber].Add(new QuestionGroupModel
            {
                Id = questionGroup.Id,
                ExamId = questionGroup.ExamId,
                PartNumber = questionGroup.PartNumber,
                ImageFilesUrl = questionGroup.ImageFilesUrl,
                AudioFilesUrl = questionGroup.AudioFilesUrl,
                Questions = questionGroup.Questions.Select(q => new QuestionModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    AnswerA = q.AnswerA,
                    AnswerB = q.AnswerB,
                    AnswerC = q.AnswerC,
                    AnswerD = q.AnswerD,
                    CorrectAnswer = q.CorrectAnswer,
                    QuestionNumber = q.QuestionNumber,
                    GroupId = q.GroupId
                }).ToList()
            });
        }

        return View("Start", takeTestModel);
    }

    [HttpGet("{id}/tabs/{activeTab}")]
    public async Task<IActionResult> DetailExamTab(string id, string activeTab = "practice")
    {
        var exam = await FetchExamAsync(id);
        ViewBag.ActiveTab = activeTab;
        return View("Detail", exam);
    }

    [HttpGet("{partId}/questions")]
    public async Task<IActionResult> GetQuestionsByPartId(string partId)
    {
        var questions = await FetchQuestionsAsync(partId);

        return PartialView("_QuestionWrapper", questions);
    }

    public IActionResult LoadTabContent(string activeTab)
    {
        ViewBag.ActiveTab = activeTab;

        return activeTab switch
        {
            "practice" => PartialView("_PracticeTab"),
            "takeTest" => PartialView("_TakeExamTab"),
            "discussion" => PartialView("_DiscussionTab"),
            _ => PartialView("_PracticeTab")
        };
    }
}
