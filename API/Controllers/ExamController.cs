using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.ExamDto;
using API.Dtos.QuestionDto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/exam")]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly IPartRepository _partRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public ExamController(IExamRepository examRepository, IPartRepository partRepository, IQuestionRepository questionRepository)
        {
            _partRepository = partRepository;
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }
        [HttpGet("getAllExams")]
        public async Task<IActionResult> GetAllExams()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var exams = await _examRepository.GetAllExams();

            return Ok(exams);
        }
        [HttpPost("createExam")]
        public async Task<IActionResult> CreateExam(CreateExamRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newExam = new Exam
            {
                Title = request.Title,
            };
            await _examRepository.CreateExam(newExam);
            return Ok(newExam);
        }
        [HttpGet("getExamById/{id}")]
        public async Task<IActionResult> GetExamById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var exam = await _examRepository.GetExamById(id);
            if (exam == null)
            {
                return NotFound();
            }
            var parts = await _partRepository.GetPartsByExamId(id);
            var questions = new List<QuestionResponseDto>();
            foreach (var part in parts)
            {
                var partQuestions = await _questionRepository.GetQuestionsByPartId(part.Id);
                if (partQuestions != null)
                {
                    questions.AddRange(partQuestions.Select(q => new QuestionResponseDto
                    {
                        Title = q.Title,
                        AnswerA = q.AnswerA,
                        AnswerB = q.AnswerB,
                        AnswerC = q.AnswerC,
                        AnswerD = q.AnswerD,
                        CorrectAnswer = q.CorrectAnswer,
                        QuestionNumber = q.QuestionNumber,
                        PartId = q.PartId,
                        PartNumber = part.PartNumber
                    }));
                }
            }


            return Ok(new GetAllExamDto
            {
                Id = exam.Id,
                Title = exam.Title,
                CreatedAt = exam.CreatedAt,
                Questions = questions
            });
        }
    }
}