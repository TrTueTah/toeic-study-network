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
    public async Task<List<ExamModel>> FetchExamsAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/exam/getAllExams");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var exams = JsonConvert.DeserializeObject<List<ExamModel>>(responseString);

        return exams;
    }
    [NonAction]
    public async Task<ExamModel> FetchExamAsync(string id)
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
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/exam/getPartsByExamId/{id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var parts = JsonConvert.DeserializeObject<List<PartModel>>(responseString);

        return parts;
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
        // var parts = await FetchPartsAsync(id);
        return View("Start");
    }
}
