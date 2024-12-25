using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Dtos;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var exams = await FetchAllExams();
        exams = exams.OrderByDescending(exam => exam.CreatedAt).ToList();
        return View(exams);
    }
    
    [HttpPost("createExam")]
    public async Task<IActionResult> CreateExam([FromBody] CreateExamRequest request)
    {
        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.ExamSeriesId))
        {
            return BadRequest("Invalid data");
        }

        var apiRequest = new
        {
            title = request.Title,
            examSeriesId = request.ExamSeriesId
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5112/api/v1/exam/createExam", apiRequest);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        var createdExam = JsonConvert.DeserializeObject<ExamModel>(await response.Content.ReadAsStringAsync());
        return RedirectToAction("CreateTest", new { examId = createdExam?.Id });
    }

    [NonAction]
    private async Task<List<ExamModel>> FetchAllExams()
    {
        var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/exam/getAllExams");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var exams = JsonConvert.DeserializeObject<List<ExamModel>>(responseString);

        return exams;
    }

    [NonAction]
    private async Task<ExamModel> FetchExamById(string? examId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/exam/getExamById/{examId}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var exam = JsonConvert.DeserializeObject<ExamModel>(responseString);

        return exam;
    }

    [HttpGet("{examId}/create")]
    public async Task<IActionResult> CreateTest(string examId)
    {
        var exam = await FetchExamById(examId);
        return View("CreateTest", exam);
    }
}
