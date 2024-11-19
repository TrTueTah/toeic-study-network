using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.QuestionDto;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/question")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IPartRepository _partRepository;
        public QuestionController(IQuestionRepository questionRepository, IPartRepository partRepository)
        {
            _questionRepository = questionRepository;
            _partRepository = partRepository;
        }

        [HttpGet("getAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();
            return Ok(questions);
        }

        [HttpGet("getQuestionById/{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var question = await _questionRepository.GetQuestionById(id);
            return Ok(question);
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            var question = new Question
            {
                Title = createQuestionDto.Title,
                AnswerA = createQuestionDto.AnswerA,
                AnswerB = createQuestionDto.AnswerB,
                AnswerC = createQuestionDto.AnswerC,
                AnswerD = createQuestionDto.AnswerD,
                CorrectAnswer = createQuestionDto.CorrectAnswer,
                QuestionNumber = createQuestionDto.QuestionNumber,
                PartId = createQuestionDto.PartId
            };
            await _questionRepository.AddQuestion(question);
            return Ok(question);
        }

        [HttpGet("getQuestionsByPartId/{partId}")]
        public async Task<IActionResult> GetQuestionsByPartId(string partId)
        {
            var questions = await _questionRepository.GetQuestionsByPartId(partId);
            return Ok(questions);
        }

        [HttpPost("upload-reading")]
        public async Task<IActionResult> UploadFileReading([FromForm] UploadReadingDto uploadReadingDto)
        {
            if (uploadReadingDto.QuestionFile == null || uploadReadingDto.QuestionFile.Length == 0 || uploadReadingDto.AnswerFile == null || uploadReadingDto.AnswerFile.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                // Đọc nội dung tệp từ IFormFile
                var linesFromQuestion = new List<string>();
                using (var reader = new StreamReader(uploadReadingDto.QuestionFile.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            linesFromQuestion.Add(line);
                        }
                    }
                }

                var linesFromAnswer = new List<string>();
                using (var reader = new StreamReader(uploadReadingDto.AnswerFile.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            linesFromAnswer.Add(line);
                        }
                    }
                }
                // Xử lý dữ liệu từ tệp
                var questions = _questionRepository.ExtractQuestionsFromReading(linesFromQuestion);
                var correctAnswers = ExtractCorrectAnswers(linesFromAnswer);

                var response = new List<UploadQuestionResponseDto>();

                foreach (var question in questions)
                {
                    if (correctAnswers.ContainsKey(question.QuestionNumber))
                    {
                        question.CorrectAnswer = correctAnswers[question.QuestionNumber];
                        var questionResponse = new UploadQuestionResponseDto
                        {
                            Title = question.Title,
                            AnswerA = question.AnswerA,
                            AnswerB = question.AnswerB,
                            AnswerC = question.AnswerC,
                            AnswerD = question.AnswerD,
                            CorrectAnswer = question.CorrectAnswer,
                            QuestionNumber = question.QuestionNumber,
                        };
                        response.Add(questionResponse);
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        [HttpPost("upload-listening")]
        public async Task<IActionResult> UploadFileListening([FromForm] UploadListeningDto uploadListeningDto)
        {
            if (uploadListeningDto.QuestionFile == null || uploadListeningDto.QuestionFile.Length == 0 || uploadListeningDto.AnswerFile == null || uploadListeningDto.AnswerFile.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                // Đọc nội dung tệp từ IFormFile
                var linesFromQuestion = new List<string>();
                using (var reader = new StreamReader(uploadListeningDto.QuestionFile.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            linesFromQuestion.Add(line);
                        }
                    }
                }

                var linesFromAnswer = new List<string>();
                using (var reader = new StreamReader(uploadListeningDto.AnswerFile.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            linesFromAnswer.Add(line);
                        }
                    }
                }
                // Xử lý dữ liệu từ tệp
                var questions = _questionRepository.ExtractQuestionsFromListening(linesFromQuestion);
                var correctAnswers = ExtractCorrectAnswers(linesFromAnswer);

                for (int i = 1; i <= 6; i++)
                {
                    var question = new Question
                    {
                        QuestionNumber = i,
                        Title = "",
                        AnswerA = "",
                        AnswerB = "",
                        AnswerC = "",
                        AnswerD = "",
                        CorrectAnswer = correctAnswers[i],
                        PartId = "",
                    };
                    questions.Add(question);
                }

                for (int i = 7; i <= 31; i++)
                {
                    var question = new Question
                    {
                        QuestionNumber = i,
                        Title = "",
                        AnswerA = "",
                        AnswerB = "",
                        AnswerC = "",
                        AnswerD = null,
                        CorrectAnswer = correctAnswers[i],
                        PartId = "",
                    };
                    questions.Add(question);
                }

                var response = new List<UploadQuestionResponseDto>();

                foreach (var question in questions)
                {
                    if (correctAnswers.ContainsKey(question.QuestionNumber))
                    {
                        question.CorrectAnswer = correctAnswers[question.QuestionNumber];
                        var questionResponse = new UploadQuestionResponseDto
                        {
                            Title = question.Title,
                            AnswerA = question.AnswerA,
                            AnswerB = question.AnswerB,
                            AnswerC = question.AnswerC,
                            AnswerD = question.AnswerD,
                            CorrectAnswer = question.CorrectAnswer,
                            QuestionNumber = question.QuestionNumber,
                        };
                        response.Add(questionResponse);
                    }
                }


                // Ghi kết quả vào tệp output.txt
                WriteQuestionsToFile("output.txt", response);


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        private void WriteQuestionsToFile(string outputPath, List<UploadQuestionResponseDto> questions)
        {
            using var writer = new StreamWriter(outputPath);
            foreach (var question in questions)
            {
                writer.WriteLine($"{question.QuestionNumber}. {question.Title}");
                writer.WriteLine($"(A) {question.AnswerA}");
                writer.WriteLine($"(B) {question.AnswerB}");
                writer.WriteLine($"(C) {question.AnswerC}");
                writer.WriteLine($"(D) {question.AnswerD}");
                writer.WriteLine($"Đáp án: {question.CorrectAnswer}");
                writer.WriteLine();
            }
        }
        private Dictionary<int, string> ExtractCorrectAnswers(List<string> lines)
        {
            var correctAnswers = new Dictionary<int, string>();

            foreach (var line in lines)
            {
                // Tách phần số thứ tự và đáp án từ dòng
                var parts = line.Split(' ');

                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int questionNumber) &&
                    parts[1].StartsWith("(") &&
                    parts[1].EndsWith(")"))
                {
                    // Loại bỏ dấu ngoặc để lấy đáp án
                    var answer = parts[1].Trim('(', ')');
                    correctAnswers[questionNumber] = answer;
                }
                else
                {
                    Console.WriteLine($"Dòng không hợp lệ: {line}");
                }
            }

            return correctAnswers;
        }
    }
}