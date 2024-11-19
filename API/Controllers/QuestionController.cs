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
        [HttpPost("upload-healthcheck")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                // Đọc nội dung tệp từ IFormFile
                var lines = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            lines.Add(line);
                        }
                    }
                }

                // Xử lý dữ liệu từ tệp
                var questions = _questionRepository.ExtractQuestionsFromLines(lines);
                // Ghi kết quả vào tệp output.txt
                WriteQuestionsToFile("output.txt", questions);

                return Ok(new { Count = questions.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> CreateQuestionFromFile([FromForm] UploadQuestionDto uploadQuestionDto)
        {
            if (uploadQuestionDto.File == null || uploadQuestionDto.File.Length == 0)
            {
                return BadRequest("File không hợp lệ.");
            }

            try
            {
                // Đọc nội dung tệp từ IFormFile
                var lines = new List<string>();
                using (var reader = new StreamReader(uploadQuestionDto.File.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            lines.Add(line);
                        }
                    }
                }

                var partMap = new Dictionary<int, string>();
                foreach (var partId in uploadQuestionDto.PartIds)
                {
                    var part = await _partRepository.GetPartById(partId);
                    if (part == null)
                    {
                        return BadRequest($"Không tìm thấy Part với Id: {partId}");
                    }
                    partMap.Add(part.PartNumber, partId);
                }

                var questions = _questionRepository.ExtractQuestionsFromLines(lines);
                var questionsResponse = new List<QuestionResponseDto>();
                var partNumber = 0;
                foreach (var question in questions)
                {
                    if (question.QuestionNumber >= 101 && question.QuestionNumber <= 130)
                    {
                        question.PartId = partMap.GetValueOrDefault(5);
                        partNumber = 5;
                    }
                    else if (question.QuestionNumber >= 131 && question.QuestionNumber <= 146)
                    {
                        question.PartId = partMap.GetValueOrDefault(6);
                        partNumber = 6;
                    }
                    else if (question.QuestionNumber >= 147 && question.QuestionNumber <= 200)
                    {
                        question.PartId = partMap.GetValueOrDefault(7);
                        partNumber = 7;
                    }

                    await _questionRepository.AddQuestion(question);
                    questionsResponse.Add(new QuestionResponseDto
                    {
                        Title = question.Title,
                        AnswerA = question.AnswerA,
                        AnswerB = question.AnswerB,
                        AnswerC = question.AnswerC,
                        AnswerD = question.AnswerD,
                        CorrectAnswer = question.CorrectAnswer,
                        QuestionNumber = question.QuestionNumber,
                        PartId = question.PartId,
                        PartNumber = partNumber
                    });
                }

                return Ok(questionsResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void WriteQuestionsToFile(string outputPath, List<Question> questions)
        {
            using var writer = new StreamWriter(outputPath);
            foreach (var question in questions)
            {
                writer.WriteLine($"{question.QuestionNumber}. {question.Title}");
                writer.WriteLine($"(A) {question.AnswerA}");
                writer.WriteLine($"(B) {question.AnswerB}");
                writer.WriteLine($"(C) {question.AnswerC}");
                writer.WriteLine($"(D) {question.AnswerD}");
                writer.WriteLine();
            }
        }
    }
}