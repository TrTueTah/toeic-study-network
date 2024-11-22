using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Dtos.QuestionGroupDto;
using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/questionGroup")]
    [ApiController]
    public class QuestionGroupController : ControllerBase
    {
        private readonly IQuestionGroupRepository _questionGroupRepository;
        private readonly FirebaseService _firebaseService;

        public QuestionGroupController(IQuestionGroupRepository questionGroupRepository, FirebaseService firebaseService)
        {
            _questionGroupRepository = questionGroupRepository;
            _firebaseService = firebaseService;
        }
        
        [HttpPost("createQuestionGroup")]
        [ProducesResponseType(typeof(CreateQuestionGroupDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CreateQuestionGroupDto>> CreateQuestionGroup([FromForm] CreateQuestionGroupDto questionGroupDto)
        {
            if (questionGroupDto == null)
            {
                return BadRequest("QuestionGroup data is null.");
            }

            try
            {
                var questionGroup = new QuestionGroup
                {
                    PartNumber = questionGroupDto.PartNumber,
                    ExamId = questionGroupDto.ExamId,
                    Questions = questionGroupDto.Questions.Select(q => new Question
                    {
                        Title = q.Title,
                        AnswerA = q.AnswerA,
                        AnswerB = q.AnswerB,
                        AnswerC = q.AnswerC,
                        AnswerD = q.AnswerD,
                        CorrectAnswer = q.CorrectAnswer,
                        QuestionNumber = q.QuestionNumber
                    }).ToList()
                };
                
                var imageUrls = new List<string>();
                var audioUrls = new List<string>();

                // Handle image and audio file uploads
                if (questionGroupDto.ImageFiles != null && questionGroupDto.ImageFiles.Count > 0)
                {
                    foreach (IFormFile file in questionGroupDto.ImageFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var imageUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{questionGroup.Id}/images");
                            imageUrls.Add(imageUrl);
                        }
                    }
                }

                if (questionGroupDto.AudioFiles != null && questionGroupDto.AudioFiles.Count > 0)
                {
                    foreach (IFormFile file in questionGroupDto.AudioFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var audioUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{questionGroup.Id}/audio");
                            audioUrls.Add(audioUrl);
                        }
                    }
                }
                
                questionGroup.ImageFilesUrl = imageUrls;
                questionGroup.AudioFilesUrl = audioUrls;

                var createdQuestionGroup = _questionGroupRepository.CreateQuestionGroup(questionGroup);
                
                return CreatedAtAction(nameof(GetQuestionGroupById), new { id = createdQuestionGroup.Id }, createdQuestionGroup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("getQuestionGroupById/{id}")]
        public ActionResult<QuestionGroup> GetQuestionGroupById(string id)
        {
            var questionGroup = _questionGroupRepository.GetQuestionGroupById(id);
            if (questionGroup == null)
            {
                return NotFound($"QuestionGroup with ID {id} not found.");
            }

            return Ok(questionGroup);
        }
        
        [HttpGet("getQuestionGroupsForExam/{examId}")]
        public ActionResult<List<QuestionGroup>> GetAllQuestionGroupsForExam(string examId)
        {
            var questionGroups = _questionGroupRepository.GetAllQuestionGroupsForExam(examId);
            if (questionGroups == null || !questionGroups.Any())
            {
                return NotFound($"No question groups found for the exam with ID {examId}.");
            }

            return Ok(questionGroups);
        }
        
        [HttpPut("updateQuestionGroup/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<QuestionGroup>> UpdateQuestionGroup(string id, [FromForm] CreateQuestionGroupDto questionGroupDto)
        {
            if (questionGroupDto == null)
            {
                return BadRequest("QuestionGroup data is null.");
            }

            var existingQuestionGroup = _questionGroupRepository.GetQuestionGroupById(id);
            if (existingQuestionGroup == null)
            {
                return NotFound($"QuestionGroup with ID {id} not found.");
            }

            try
            {
                existingQuestionGroup.PartNumber = questionGroupDto.PartNumber;
                existingQuestionGroup.ExamId = questionGroupDto.ExamId;
                existingQuestionGroup.Questions = questionGroupDto.Questions.Select(q => new Question
                {
                    Title = q.Title,
                    AnswerA = q.AnswerA,
                    AnswerB = q.AnswerB,
                    AnswerC = q.AnswerC,
                    AnswerD = q.AnswerD,
                    CorrectAnswer = q.CorrectAnswer,
                    QuestionNumber = q.QuestionNumber
                }).ToList();
                
                var imageUrls = new List<string>();
                var audioUrls = new List<string>();

                if (questionGroupDto.ImageFiles != null && questionGroupDto.ImageFiles.Count > 0)
                {
                    foreach (IFormFile file in questionGroupDto.ImageFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var imageUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{id}/images");
                            imageUrls.Add(imageUrl);
                        }
                    }
                }

                if (questionGroupDto.AudioFiles != null && questionGroupDto.AudioFiles.Count > 0)
                {
                    foreach (IFormFile file in questionGroupDto.AudioFiles)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var audioUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{id}/audio");
                            audioUrls.Add(audioUrl);
                        }
                    }
                }
                
                existingQuestionGroup.ImageFilesUrl = imageUrls;
                existingQuestionGroup.AudioFilesUrl = audioUrls;

                var updatedQuestionGroup = _questionGroupRepository.UpdateQuestionGroup(existingQuestionGroup);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpDelete("deleteQuestionGroup/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteQuestionGroup(string id)
        {
            var exists = _questionGroupRepository.QuestionGroupExists(id);
            if (!exists)
            {
                return NotFound($"QuestionGroup with ID {id} not found.");
            }

            try
            {
                var result = _questionGroupRepository.DeleteQuestionGroup(id);
                if (!result)
                {
                    return StatusCode(500, "A problem happened while deleting the question group.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
