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
        return View(exams);
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
    public async Task<List<PartModel>> FetchPartsAsync(string id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/part/getPartsByExamId/{id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var parts = JsonConvert.DeserializeObject<List<PartModel>>(responseString);

        return parts;
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
        var parts = await FetchPartsAsync(id);
        var exam = await FetchExamAsync(id);
        TakeTestModel takeTestModel = new TakeTestModel
        {
            TestType = "Full Test",
            Title = exam.Title,
            PartModels = parts,
        };
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
