using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly ILogger<ExamController> _logger;

        public ExamController(IExamRepository examRepository, ILogger<ExamController> logger)
        {
            _examRepository = examRepository;
            _logger = logger;
        }

        // GET: api/exam/getAllExams
        [HttpGet("getAllExams")]
        [ProducesResponseType(typeof(List<Exam>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Exam>>> GetAllExams()
        {
            try
            {
                var exams = await _examRepository.GetAllExams();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/exam/getExamById/{id}
        [HttpGet("getExamById/{id}")]
        [ProducesResponseType(typeof(Exam), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Exam>> GetExamById(string id)
        {
            try
            {
                var exam = await _examRepository.GetExamById(id);
                
                if (exam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }
                
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/exam/createExam
        [HttpPost("createExam")]
        [ProducesResponseType(typeof(Exam), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Exam>> CreateExam([FromBody] Exam exam)
        {
            try
            {
                if (exam == null)
                {
                    return BadRequest("Exam data is null.");
                }
                
                var createdExam = await _examRepository.CreateExam(exam);
                return CreatedAtAction(nameof(GetExamById), new { id = createdExam.Id }, createdExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
