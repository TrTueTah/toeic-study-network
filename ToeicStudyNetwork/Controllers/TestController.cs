using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Dtos;
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> DetailExam(string id)
    {
        try
        {
            var userId = Request.Cookies["userId"];
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is missing in cookies.");
            }

            var userResults = await FetchUserResultByUserId(userId);

            var exam = await FetchExamAsync(id);

            var testDetailViewModel = new TestDetailViewModel
            {
                Exam = exam,
                UserResults = userResults
            };

            return View("Detail", testDetailViewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet("{examId}/practice")]
    public async Task<IActionResult> PracticeExam(string examId, [FromQuery] List<int>? partNumbers)
    {
        if (string.IsNullOrEmpty(examId))
        {
            return BadRequest("Exam ID is required.");
        }

        if (partNumbers == null || partNumbers.Count == 0)
        {
            return BadRequest("At least one part number must be provided.");
        }

        try
        {
            var exam = await FetchExamByParts(examId, partNumbers);

            if (exam == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to parse exam data.");
            }

            var practiceExamModel = new TestPracticeViewModel
            {
                Id = exam.ExamId,
                TestType = "Part " + string.Join(", ", exam.PartNumbers),
                PartNumbers = exam.PartNumbers,
                Title = exam.Title,
                PartQuestions = new Dictionary<int, List<QuestionGroupModel>>(),
                TimeLimit = new TimeSpan(2, 0, 0)
            };

            foreach (var questionGroup in exam.QuestionGroups)
            {
                if (!practiceExamModel.PartQuestions.ContainsKey(questionGroup.PartNumber))
                {
                    practiceExamModel.PartQuestions[questionGroup.PartNumber] = new List<QuestionGroupModel>();
                }

                practiceExamModel.PartQuestions[questionGroup.PartNumber].Add(new QuestionGroupModel
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

            return View("Practice", practiceExamModel);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, $"External API error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet("{examId}/start")]
    public async Task<IActionResult> StartExam(string examId)
    {
        var exam = await FetchExamAsync(examId);

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
    private async Task<List<UserResultViewModel>> FetchUserResultByUserId(string userId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/result/getUserResultByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error fetching user results: {response.ReasonPhrase}");
        }

        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<UserResultViewModel>>(responseData);
    }
    
    [NonAction]
    private async Task<TestPracticeResponse?> FetchExamByParts(string examId, List<int> partNumbers)
    {
        var queryString = string.Join("&", partNumbers.Select(p => $"partNumbers={p}"));
        var url = $"http://localhost:5112/api/v1/exam/getExamByPart/{examId}?{queryString}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to fetch exam by parts. Status code: {response.StatusCode}");
        }

        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TestPracticeResponse>(responseData);
    }
    
}
