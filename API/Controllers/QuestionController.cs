using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.QuestionDto;
using API.Dtos.QuestionGroupDto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Exception = System.Exception;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/question")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IQuestionGroupRepository _questionGroupRepository;
        private readonly IExamRepository _examRepository;
        
        public QuestionController(IQuestionRepository questionRepository, IMapper mapper, IQuestionGroupRepository questionGroupRepository, IExamRepository examRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _questionGroupRepository = questionGroupRepository;
            _examRepository = examRepository;
        }

        [HttpPost("createQuestionList")]
        
        public IActionResult createQuestionList([FromBody] CreateQuestionListDto createQuestionListDto)
        {
            if (createQuestionListDto == null)
            {
                return BadRequest("Question list data is null.");
            }

            try
            {
                var existingExam = _examRepository.GetExamById(createQuestionListDto.ExamId);
                
                if (existingExam != null && existingExam.QuestionGroups.Any())
                {
                    return BadRequest("Exam already contains question groups.");
                }
                
                List<QuestionGroupDto> questionGroups = new List<QuestionGroupDto>
                {
                    new QuestionGroupDto { Questions = new List<int> { 1 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 2 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 3 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 4 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 5 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 6 }, PartNumber = 1 },
                    new QuestionGroupDto { Questions = new List<int> { 7 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 8 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 9 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 10 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 11 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 12 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 13 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 14 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 15 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 16 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 17 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 18 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 19 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 20 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 21 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 22 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 23 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 24 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 25 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 26 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 27 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 28 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 29 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 30 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 31 }, PartNumber = 2 },
                    new QuestionGroupDto { Questions = new List<int> { 32, 33, 34 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 35, 36, 37 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 38, 39, 40 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 41, 42, 43 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 44, 45, 46 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 47, 48, 49 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 50, 51, 52 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 53, 54, 55 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 56, 57, 58 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 59, 60, 61 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 62, 63, 64 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 65, 66, 67 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 68, 69, 70 }, PartNumber = 3 },
                    new QuestionGroupDto { Questions = new List<int> { 71, 72, 73 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 74, 75, 76 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 77, 78, 79 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 80, 81, 82 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 83, 84, 85 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 86, 87, 88 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 89, 90, 91 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 92, 93, 94 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 95, 96, 97 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 98, 99, 100 }, PartNumber = 4 },
                    new QuestionGroupDto { Questions = new List<int> { 101 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 102 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 103 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 104 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 105 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 106 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 107 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 108 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 109 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 110 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 111 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 112 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 113 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 114 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 115 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 116 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 117 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 118 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 119 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 120 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 121 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 122 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 123 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 124 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 125 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 126 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 127 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 128 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 129 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 130 }, PartNumber = 5 },
                    new QuestionGroupDto { Questions = new List<int> { 131, 132, 133, 134 }, PartNumber = 6 },
                    new QuestionGroupDto { Questions = new List<int> { 135, 136, 137, 138 }, PartNumber = 6 },
                    new QuestionGroupDto { Questions = new List<int> { 139, 140, 141, 142 }, PartNumber = 6 },
                    new QuestionGroupDto { Questions = new List<int> { 143, 144, 145, 146 }, PartNumber = 6 },
                    new QuestionGroupDto { Questions = new List<int> { 147, 148 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 149, 150 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 151, 152 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 153, 154 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 155, 156, 157 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 158, 159, 160 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 161, 162, 163 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 164, 165, 166, 167 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 168, 169, 170, 171 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 172, 173, 174, 175 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 176, 177, 178, 179, 180 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 181, 182, 183, 184, 185 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 186, 187, 188, 189, 190 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 191, 192, 193, 194, 195 }, PartNumber = 7 },
                    new QuestionGroupDto { Questions = new List<int> { 196, 197, 198, 199, 200 }, PartNumber = 7 }
                };
                

                var groupedQuestions = new List<QuestionGroup>();
                
                foreach (var group in questionGroups)
                {
                    var newGroup = new QuestionGroup
                    {
                        PartNumber = group.PartNumber,
                        ExamId = createQuestionListDto.ExamId,
                        Questions = new List<Question>()
                    };
                    
                    foreach (var questionId in group.Questions)
                    {
                        var questionData = createQuestionListDto.Questions.FirstOrDefault(q => q.QuestionNumber == questionId);

                        if (questionData != null)
                        {
                            var newQuestion = new Question
                            {
                                QuestionNumber = questionData.QuestionNumber,
                                Title = questionData.Title,
                                AnswerA = questionData.AnswerA,
                                AnswerB = questionData.AnswerB,
                                AnswerC = questionData.AnswerC,
                                AnswerD = questionData.AnswerD,
                                CorrectAnswer = questionData.CorrectAnswer
                            };

                            newGroup.Questions.Add(newQuestion);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                    groupedQuestions.Add(newGroup);
                }
                
                foreach (var group in groupedQuestions)
                {
                    _questionGroupRepository.CreateQuestionGroup(group);
                }
                
                var response = new
                {
                    ExamId = groupedQuestions.FirstOrDefault()?.ExamId,
                    QuestionGroups = groupedQuestions.OrderBy(g => g.PartNumber).ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

//         [HttpGet("getAllQuestions")]
//         public async Task<IActionResult> GetAllQuestions()
//         {
//             var questions = await _questionRepository.GetAllQuestions();
//             return Ok(questions);
//         }
//
//         [HttpGet("getQuestionById/{id}")]
//         public async Task<IActionResult> GetQuestionById(string id)
//         {
//             var question = await _questionRepository.GetQuestionById(id);
//             return Ok(question);
//         }
//
//         [Authorize(Roles = "Admin")]
//         [HttpPost("createQuestion")]
//         public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
//         {
//             var question = new Question
//             {
//                 Title = createQuestionDto.Title,
//                 AnswerA = createQuestionDto.AnswerA,
//                 AnswerB = createQuestionDto.AnswerB,
//                 AnswerC = createQuestionDto.AnswerC,
//                 AnswerD = createQuestionDto.AnswerD,
//                 CorrectAnswer = createQuestionDto.CorrectAnswer,
//                 QuestionNumber = createQuestionDto.QuestionNumber,
//                 PartId = createQuestionDto.PartId
//             };
//             await _questionRepository.AddQuestion(question);
//             return Ok(question);
//         }
//
//         [HttpGet("getQuestionsByPartId/{partId}")]
//         public async Task<IActionResult> GetQuestionsByPartId(string partId)
//         {
//             var questions = await _questionRepository.GetQuestionsByPartId(partId);
//             return Ok(questions);
//         }
//
//         [Authorize(Roles = "Admin")]
//         [HttpPost("upload-reading")]
//         public async Task<IActionResult> UploadFileReading([FromForm] UploadReadingDto uploadReadingDto)
//         {
//             if (uploadReadingDto.QuestionFile == null || uploadReadingDto.QuestionFile.Length == 0 || uploadReadingDto.AnswerFile == null || uploadReadingDto.AnswerFile.Length == 0)
//             {
//                 return BadRequest("File không hợp lệ.");
//             }
//
//             try
//             {
//                 // Đọc nội dung tệp từ IFormFile
//                 var linesFromQuestion = new List<string>();
//                 using (var reader = new StreamReader(uploadReadingDto.QuestionFile.OpenReadStream()))
//                 {
//                     while (!reader.EndOfStream)
//                     {
//                         var line = await reader.ReadLineAsync();
//                         if (line != null)
//                         {
//                             linesFromQuestion.Add(line);
//                         }
//                     }

//                 }
//
//                 var linesFromAnswer = new List<string>();
//                 using (var reader = new StreamReader(uploadReadingDto.AnswerFile.OpenReadStream()))
//                 {
//                     while (!reader.EndOfStream)
//                     {
//                         var line = await reader.ReadLineAsync();
//                         if (line != null)
//                         {
//                             linesFromAnswer.Add(line);
//                         }
//                     }
//                 }
//                 // Xử lý dữ liệu từ tệp
//                 var questions = _questionRepository.ExtractQuestionsFromReading(linesFromQuestion);
//                 var correctAnswers = ExtractCorrectAnswers(linesFromAnswer);
//
//                 var response = new List<UploadQuestionResponseDto>();
//
//                 foreach (var question in questions)
//                 {
//                     if (correctAnswers.ContainsKey(question.QuestionNumber))
//                     {
//                         question.CorrectAnswer = correctAnswers[question.QuestionNumber];
//                         var questionResponse = new UploadQuestionResponseDto
//                         {
//                             Title = question.Title,
//                             AnswerA = question.AnswerA,
//                             AnswerB = question.AnswerB,
//                             AnswerC = question.AnswerC,
//                             AnswerD = question.AnswerD,
//                             CorrectAnswer = question.CorrectAnswer,
//                             QuestionNumber = question.QuestionNumber,
//                         };
//                         response.Add(questionResponse);
//                     }
//                 }
//                 return Ok(response);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
//             }
//         }
//
//         [Authorize(Roles = "Admin")]
//         [HttpPost("upload-listening")]
//         public async Task<IActionResult> UploadFileListening([FromForm] UploadListeningDto uploadListeningDto)
//         {
//             if (uploadListeningDto.QuestionFile == null || uploadListeningDto.QuestionFile.Length == 0 || uploadListeningDto.AnswerFile == null || uploadListeningDto.AnswerFile.Length == 0)
//             {
//                 return BadRequest("File không hợp lệ.");
//             }
//
//             try
//             {
//                 // Đọc nội dung tệp từ IFormFile
//                 var linesFromQuestion = new List<string>();
//                 using (var reader = new StreamReader(uploadListeningDto.QuestionFile.OpenReadStream()))
//                 {
//                     while (!reader.EndOfStream)
//                     {
//                         var line = await reader.ReadLineAsync();
//                         if (line != null)
//                         {
//                             linesFromQuestion.Add(line);
//                         }
//                     }
//                 }
//
//                 var linesFromAnswer = new List<string>();
//                 using (var reader = new StreamReader(uploadListeningDto.AnswerFile.OpenReadStream()))
//                 {
//                     while (!reader.EndOfStream)
//                     {
//                         var line = await reader.ReadLineAsync();
//                         if (line != null)
//                         {
//                             linesFromAnswer.Add(line);
//                         }
//                     }
//                 }
//                 // Xử lý dữ liệu từ tệp
//                 var questions = _questionRepository.ExtractQuestionsFromListening(linesFromQuestion);
//                 var correctAnswers = ExtractCorrectAnswers(linesFromAnswer);
//
//                 for (int i = 1; i <= 6; i++)
//                 {
//                     var question = new Question
//                     {
//                         QuestionNumber = i,
//                         Title = "",
//                         AnswerA = "",
//                         AnswerB = "",
//                         AnswerC = "",
//                         AnswerD = "",
//                         CorrectAnswer = correctAnswers[i],
//                         PartId = "",
//                     };
//                     questions.Add(question);
//                 }
//
//                 for (int i = 7; i <= 31; i++)
//                 {
//                     var question = new Question
//                     {
//                         QuestionNumber = i,
//                         Title = "",
//                         AnswerA = "",
//                         AnswerB = "",
//                         AnswerC = "",
//                         AnswerD = null,
//                         CorrectAnswer = correctAnswers[i],
//                         PartId = "",
//                     };
//                     questions.Add(question);
//                 }
//
//                 var response = new List<UploadQuestionResponseDto>();
//
//                 foreach (var question in questions)
//                 {
//                     if (correctAnswers.ContainsKey(question.QuestionNumber))
//                     {
//                         question.CorrectAnswer = correctAnswers[question.QuestionNumber];
//                         var questionResponse = new UploadQuestionResponseDto
//                         {
//                             Title = question.Title,
//                             AnswerA = question.AnswerA,
//                             AnswerB = question.AnswerB,
//                             AnswerC = question.AnswerC,
//                             AnswerD = question.AnswerD,
//                             CorrectAnswer = question.CorrectAnswer,
//                             QuestionNumber = question.QuestionNumber,
//                         };
//                         response.Add(questionResponse);
//                     }
//                 }
//
//
//                 // Ghi kết quả vào tệp output.txt
//                 WriteQuestionsToFile("output.txt", response);
//
//
//                 return Ok(response);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
//             }
//         }
//         private void WriteQuestionsToFile(string outputPath, List<UploadQuestionResponseDto> questions)
//         {
//             using var writer = new StreamWriter(outputPath);
//             foreach (var question in questions)
//             {
//                 writer.WriteLine($"{question.QuestionNumber}. {question.Title}");
//                 writer.WriteLine($"(A) {question.AnswerA}");
//                 writer.WriteLine($"(B) {question.AnswerB}");
//                 writer.WriteLine($"(C) {question.AnswerC}");
//                 writer.WriteLine($"(D) {question.AnswerD}");
//                 writer.WriteLine($"Đáp án: {question.CorrectAnswer}");
//                 writer.WriteLine();
//             }
//         }
//         private Dictionary<int, string> ExtractCorrectAnswers(List<string> lines)
//         {
//             var correctAnswers = new Dictionary<int, string>();
//
//             foreach (var line in lines)
//             {
//                 // Tách phần số thứ tự và đáp án từ dòng
//                 var parts = line.Split(' ');
//
//                 if (parts.Length == 2 &&
//                     int.TryParse(parts[0], out int questionNumber) &&
//                     parts[1].StartsWith("(") &&
//                     parts[1].EndsWith(")"))
//                 {
//                     // Loại bỏ dấu ngoặc để lấy đáp án
//                     var answer = parts[1].Trim('(', ')');
//                     correctAnswers[questionNumber] = answer;
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Dòng không hợp lệ: {line}");
//                 }
//             }
//
//             return correctAnswers;
//         }
     }
}