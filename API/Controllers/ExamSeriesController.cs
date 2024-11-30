using API.Interfaces;
using API.Models;
using API.Dtos.ExamSeriesDto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/examSeries")]
    [ApiController]
    public class ExamSeriesController : ControllerBase
    {
        private readonly IExamSeriesRepository _examSeriesRepository;
        private readonly IMapper _mapper;

        public ExamSeriesController(IExamSeriesRepository examSeriesRepository, IMapper mapper)
        {
            _examSeriesRepository = examSeriesRepository;
            _mapper = mapper;
        }

        // GET: api/v1/examSeries/getAllExamSeries
        [HttpGet("getAllExamSeries")]
        [ProducesResponseType(typeof(List<ExamSeries>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<List<ExamSeries>> GetAllExamSeries()
        {
            try
            {
                var examSeries = _examSeriesRepository.GetAllExamSeries();
                var examSeriesDto = _mapper.Map<List<GetExamSeriesDto>>(examSeries);
                return Ok(examSeriesDto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/v1/examSeries/getExamSeriesById/{id}
        [HttpGet("getExamSeriesById/{id}")]
        [ProducesResponseType(typeof(ExamSeries), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ExamSeries> GetExamSeriesById(string id)
        {
            try
            {
                var examSeries = _examSeriesRepository.GetExamSeriesById(id);
                if (examSeries == null)
                {
                    return NotFound($"Exam series with ID {id} not found.");
                }
                var examSeriesDto = _mapper.Map<GetExamSeriesDto>(examSeries);

                return Ok(examSeries);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/v1/examSeries/createExamSeries
        [HttpPost("createExamSeries")]
        [ProducesResponseType(typeof(ExamSeries), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<ExamSeries> CreateExamSeries([FromBody] CreateExamSeriesDto examSeriesDto)
        {
            try
            {
                if (examSeriesDto == null)
                {
                    return BadRequest("Exam series data is null.");
                }

                var examSeries = _mapper.Map<ExamSeries>(examSeriesDto);
                var createdExamSeries = _examSeriesRepository.CreateExamSeries(examSeries);

                return CreatedAtAction(nameof(GetExamSeriesById), new { id = createdExamSeries.Id }, createdExamSeries);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/v1/examSeries/updateExamSeries/{id}
        [HttpPut("updateExamSeries/{id}")]
        [ProducesResponseType(typeof(ExamSeries), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ExamSeries> UpdateExamSeries(string id, [FromBody] UpdateExamSeriesDto examSeriesDto)
        {
            try
            {
                if (examSeriesDto == null || id != examSeriesDto.Id)
                {
                    return BadRequest("Exam series data is invalid.");
                }

                var existingExamSeries = _examSeriesRepository.GetExamSeriesById(id);
                if (existingExamSeries == null)
                {
                    return NotFound($"Exam series with ID {id} not found.");
                }

                var updatedExamSeries = _mapper.Map<ExamSeries>(examSeriesDto);
                var result = _examSeriesRepository.UpdateExamSeries(updatedExamSeries);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/v1/examSeries/deleteExamSeries/{id}
        [HttpDelete("deleteExamSeries/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteExamSeries(string id)
        {
            try
            {
                var examSeries = _examSeriesRepository.GetExamSeriesById(id);
                if (examSeries == null)
                {
                    return NotFound($"Exam series with ID {id} not found.");
                }

                bool isDeleted = _examSeriesRepository.DeleteExamSeries(id);
                if (!isDeleted)
                {
                    return StatusCode(500, "There was an issue deleting the exam series.");
                }

                return NoContent();  // 204 No Content
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
