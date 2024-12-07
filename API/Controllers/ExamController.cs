using System.Runtime.InteropServices.JavaScript;
using API.Dtos.ExamDto;
using API.Interfaces;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly FirebaseService _firebaseService;

        public ExamController(IExamRepository examRepository, IMapper mapper, FirebaseService firebaseService)
        {
            _examRepository = examRepository;
            _mapper = mapper;
            _firebaseService = firebaseService;
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
                var exam = _examRepository.GetExamById(id);

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
        
        [HttpPost("uploadExamAudio")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UploadExamAudio([FromForm] UpdateExamDto updateExamDto)
        {
            try
            {
                if (updateExamDto.audioFile == null || updateExamDto.audioFile.Length == 0)
                {
                    return BadRequest("Audio file is missing or empty.");
                }

                var contentType = updateExamDto.audioFile.ContentType;
                if (contentType != "audio/mp3" && contentType != "audio/mpeg")
                {
                    return BadRequest("Invalid file type. Please upload an MP3 file.");
                }

                var existingExam = _examRepository.GetExamById(updateExamDto.ExamId);
                if (existingExam == null)
                {
                    return NotFound($"Exam with ID {updateExamDto.ExamId} not found.");
                }

                // Upload file to Firebase
                string audioUrl = await _firebaseService.UploadFileAsync(updateExamDto.audioFile, $"examAudios/{updateExamDto.ExamId}");

                // Save the URL to the exam record
                existingExam.AudioFilesUrl = audioUrl;
                var exam = _mapper.Map<Exam>(existingExam);
                _examRepository.UpdateExam(exam);

                return Ok(audioUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getExamByPart/{examId}")]
        [ProducesResponseType(typeof(GetExamByPartDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<GetExamByPartDto> GetExamByPart(string examId, [FromQuery] List<int> partNumbers)
        {
            try
            {
                var result = _examRepository.GetExamByPart(examId, partNumbers);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpDelete("deleteExam/{examId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeletePost(string id)
        {
            var post = _examRepository.DeleteExam(id);
            return NoContent();
        }
    }
}
