using API.Dtos.ExamDto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamController(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
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
                
                var examDto = _mapper.Map<Exam>(exam);
                return Ok(examDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/exam/createExam
        [HttpPost("createExam")]
        [ProducesResponseType(typeof(CreateExamRequestDto), 201)] 
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CreateExamRequestDto>> CreateExam([FromBody] CreateExamRequestDto examDto)
        {
            try
            {
                if (examDto == null)
                {
                    return BadRequest("Exam data is null.");
                }
                
                var exam = _mapper.Map<Exam>(examDto);

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
