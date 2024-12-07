using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Models;
using ToeicStudyNetwork.ViewModels;
using ToeicStudyNetwork.ViewModels.Test;

namespace ToeicStudyNetwork.Controllers;

[Route("[controller]")]
public class TestController : Controller
{
    private readonly HttpClient _httpClient;
    public TestController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("Analytics")]
    public async Task<IActionResult> Analytics()
    {
        var userId = Request.Cookies["userId"];
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/result/getUserResultByUserId/{userId}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var userResults = JsonConvert.DeserializeObject<List<TestResultViewModel>>(responseString);
        var testAnalyticsModel = new TestAnalyticsViewModel
        {
            UserResults = userResults
        };
        return View(testAnalyticsModel);
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
        var testViewModel = new TestViewModel
        {
            ExamSeries = examSeries
        };
        return View(testViewModel);
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
        var testDetailViewModel = new TestDetailViewModel
        {
            Exam = exam
        };
        return View("Detail", testDetailViewModel);
    }

    [HttpGet("{id}/start")]
    public async Task<IActionResult> StartExam(string id)
    {
        var exam = await FetchExamAsync(id);

        TestStartViewModel takeTestModel = new()
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
        var testDetailViewModel = new TestDetailViewModel
        {
            Exam = exam
        };
        return View("Detail", testDetailViewModel);
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


    [HttpGet("{examId}/result/{resultId}")]
    public async Task<IActionResult> Result(string examId, string resultId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/result/getUserResult/{resultId}");

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Failed to fetch result data.");
        }

        var resultData = JsonConvert.DeserializeObject<TestResultViewModel>(await response.Content.ReadAsStringAsync());
        var userImage = Request.Cookies["userImage"];
        var username = Request.Cookies["given_name"];
        ViewBag.UserImage = userImage;
        ViewBag.Username = username;
        return View("Result", resultData);
    }

}
