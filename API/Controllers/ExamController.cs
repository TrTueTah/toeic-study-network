using API.Dtos.ExamDto;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/exam")]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
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
            return Ok(exam);
        }
    }
}