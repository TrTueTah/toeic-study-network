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
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
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
                QuenstionNumber = createQuestionDto.QuenstionNumber,
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
    }
}