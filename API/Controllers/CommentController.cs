using API.Interfaces;
using API.Models;
using API.Dtos.CommentDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly BlobService _blobService;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, BlobService blobService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _blobService = blobService;
        }

        // GET: api/v1/comment/getAllComments
        [HttpGet("getAllComments")]
        [ProducesResponseType(typeof(ICollection<CommentDto>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<ICollection<CommentDto>> GetAllComments()
        {
            var comments = _commentRepository.GetAllComments();
            var commentDtos = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentDtos);
        }

        // GET: api/v1/comment/getCommentById/{id}
        [HttpGet("getCommentById/{id}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<CommentDto> GetComment(int id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        // POST: api/v1/comment/createComment
        [HttpPost("createComment")]
        [ProducesResponseType(typeof(CommentDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CommentDto>> CreateComment([FromForm] CreateCommentDto createCommentDto)
        {
            if (createCommentDto == null)
            {
                return BadRequest("Comment is null.");
            }

            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.CreatedAt = DateTime.Now;
            comment.MediaUrls = new List<string>();

            if (!_commentRepository.CreateComment(comment))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            ICollection<string> mediaUrls = new List<string>();

            if (createCommentDto.MediaFiles != null && createCommentDto.MediaFiles.Count > 0)
            {
                foreach (IFormFile file in createCommentDto.MediaFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        var url = await _blobService.UploadFileAsync(file, $"comments/{comment.Id}");
                        mediaUrls.Add(url);
                    }
                }
            }

            comment.MediaUrls = mediaUrls;

            if (!_commentRepository.UpdateComment(comment))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdCommentDto = _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, createdCommentDto);
        }

        // PUT: api/v1/comment/updateComment/{id}
        [HttpPut("updateComment/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateComment(int id, [FromBody] CommentDto commentDto)
        {
            if (commentDto == null || commentDto.Id != id)
            {
                return BadRequest("Comment is null or ID mismatch.");
            }

            if (!_commentRepository.CommentExists(id))
            {
                return NotFound();
            }

            var comment = _mapper.Map<Comment>(commentDto);

            if (!_commentRepository.UpdateComment(comment))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        //DELETE: api/v1/comment/deleteComment/{id}
        [HttpDelete("deleteComment/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            try
            {
                await _blobService.DeleteFolderAsync($"comments/{id}");

                if (!_commentRepository.DeleteComment(id))
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return NoContent();
        }
    }
}
