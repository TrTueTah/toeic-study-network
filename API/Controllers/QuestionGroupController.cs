using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Dtos.QuestionGroupDto;
using API.Services;
using API.Dtos.QuestionDto;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/v1/questionGroup")]
    [ApiController]
    public class QuestionGroupController : ControllerBase
    {
        private readonly IQuestionGroupRepository _questionGroupRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly FirebaseService _firebaseService;

        public QuestionGroupController(IQuestionGroupRepository questionGroupRepository, FirebaseService firebaseService, IQuestionRepository questionRepository)
        {
            _questionGroupRepository = questionGroupRepository;
            _firebaseService = firebaseService;
            _questionRepository = questionRepository;
        }
        
        [HttpPost("createQuestionGroup")]
        [ProducesResponseType(typeof(CreateQuestionGroupDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CreateQuestionGroupDto>> CreateQuestionGroup(
            [FromForm] CreateQuestionGroupDto questionGroupDto
        )
        {
            if (questionGroupDto == null)
            {
                return BadRequest("QuestionGroup data is null.");
            }

            if (string.IsNullOrWhiteSpace(questionGroupDto.QuestionsJson))
            {
                return BadRequest("Questions list is required.");
            }

            try
            {
                // Parse `QuestionsJson` thành danh sách các đối tượng câu hỏi
                var questions = JsonConvert.DeserializeObject<List<CreateQuestionDto>>(questionGroupDto.QuestionsJson);
                if (questions == null || !questions.Any())
                {
                    return BadRequest("Questions list is empty or invalid.");
                }

                // Upload image files
                var imageUrls = new List<string>();
                if (questionGroupDto.ImageFiles != null)
                {
                    foreach (var file in questionGroupDto.ImageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var imageUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{questionGroupDto.ExamId}/images");
                            imageUrls.Add(imageUrl);
                        }
                    }
                }

                // Upload audio files
                var audioUrls = new List<string>();
                if (questionGroupDto.AudioFiles != null)
                {
                    foreach (var file in questionGroupDto.AudioFiles)
                    {
                        if (file.Length > 0)
                        {
                            var audioUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{questionGroupDto.ExamId}/audio");
                            audioUrls.Add(audioUrl);
                        }
                    }
                }

                // Create a new QuestionGroup
                var questionGroup = new QuestionGroup
                {
                    PartNumber = questionGroupDto.PartNumber,
                    ExamId = questionGroupDto.ExamId,
                    ImageFilesUrl = imageUrls,
                    AudioFilesUrl = audioUrls,
                    Questions = questions.Select(q => new Question
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

                // Save to database
                _questionGroupRepository.CreateQuestionGroup(questionGroup);

                return CreatedAtAction(nameof(GetQuestionGroupById), new { id = questionGroup.Id }, questionGroup);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Invalid Questions format: {ex.Message}");
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
        
        // [HttpPut("updateQuestionGroup/{id}")]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        // [ProducesResponseType(500)]
        // public async Task<ActionResult<QuestionGroup>> UpdateQuestionGroup(string id, [FromForm] CreateQuestionGroupDto questionGroupDto)
        // {
        //     if (questionGroupDto == null)
        //     {
        //         return BadRequest("QuestionGroup data is null.");
        //     }
        //
        //     var existingQuestionGroup = _questionGroupRepository.GetQuestionGroupById(id);
        //     if (existingQuestionGroup == null)
        //     {
        //         return NotFound($"QuestionGroup with ID {id} not found.");
        //     }
        //
        //     try
        //     {
        //         existingQuestionGroup.PartNumber = questionGroupDto.PartNumber;
        //         existingQuestionGroup.ExamId = questionGroupDto.ExamId;
        //         existingQuestionGroup.Questions = questionGroupDto.Questions.Select(q => new Question
        //         {
        //             Title = q.Title,
        //             AnswerA = q.AnswerA,
        //             AnswerB = q.AnswerB,
        //             AnswerC = q.AnswerC,
        //             AnswerD = q.AnswerD,
        //             CorrectAnswer = q.CorrectAnswer,
        //             QuestionNumber = q.QuestionNumber
        //         }).ToList();
        //         
        //         var imageUrls = new List<string>();
        //         var audioUrls = new List<string>();
        //
        //         if (questionGroupDto.ImageFiles != null && questionGroupDto.ImageFiles.Count > 0)
        //         {
        //             foreach (IFormFile file in questionGroupDto.ImageFiles)
        //             {
        //                 if (file != null && file.Length > 0)
        //                 {
        //                     var imageUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{id}/images");
        //                     imageUrls.Add(imageUrl);
        //                 }
        //             }
        //         }
        //
        //         if (questionGroupDto.AudioFiles != null && questionGroupDto.AudioFiles.Count > 0)
        //         {
        //             foreach (IFormFile file in questionGroupDto.AudioFiles)
        //             {
        //                 if (file != null && file.Length > 0)
        //                 {
        //                     var audioUrl = await _firebaseService.UploadFileAsync(file, $"questionGroups/{id}/audio");
        //                     audioUrls.Add(audioUrl);
        //                 }
        //             }
        //         }
        //         
        //         existingQuestionGroup.ImageFilesUrl = imageUrls;
        //         existingQuestionGroup.AudioFilesUrl = audioUrls;
        //
        //         var updatedQuestionGroup = _questionGroupRepository.UpdateQuestionGroup(existingQuestionGroup);
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }

        
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
