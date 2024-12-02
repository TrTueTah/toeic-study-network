using API.Interfaces;
using API.Models;
using API.Dtos.CommentDto;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly FirebaseService _firebaseService;
        private readonly IPostRepository _postRepository;
        public CommentController(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, FirebaseService firebaseService, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _firebaseService = firebaseService;
            _postRepository = postRepository;
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

        [HttpGet("getCommentsByPostId/{postId}")]
        [ProducesResponseType(typeof(ICollection<CommentDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<CommentResponseDto>> GetCommentsByPostId(string postId)
        {
            var comments = _commentRepository.GetCommentsByPostId(postId);
            if (comments == null) return NotFound();

            var commentDtos = new List<CommentResponseDto>();
            foreach (var comment in comments)
            {
                var user = _userRepository.GetUserById(comment.UserId);
                var commentDto = new CommentResponseDto
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    MediaUrls = comment.MediaUrls,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    Username = user.Username,
                    UserImageUrl = user.ImageUrl
                };
                commentDtos.Add(commentDto);
            }
            return Ok(commentDtos);
        }

        // GET: api/v1/comment/getCommentById/{id}
        [HttpGet("getCommentById/{id}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<CommentDto> GetComment(string id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null) return NotFound();

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
            if (createCommentDto == null) return BadRequest("Comment is null.");

            if (string.IsNullOrEmpty(createCommentDto.UserId) || !_userRepository.UserExists(createCommentDto.UserId))
            {
                return BadRequest("Invalid user ID.");
            }
            if (string.IsNullOrEmpty(createCommentDto.PostId) || !_postRepository.PostExists(createCommentDto.PostId))
            {
                return BadRequest("Invalid post ID.");
            }

            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.CreatedAt = DateTime.UtcNow;
            comment.MediaUrls = new List<string>();

            if (!_commentRepository.CreateComment(comment)) return StatusCode(500, "A problem occurred.");

            List<string> mediaUrls = new List<string>();
            if (createCommentDto.MediaFiles?.Count > 0)
            {
                foreach (IFormFile file in createCommentDto.MediaFiles)
                {
                    if (file?.Length > 0)
                    {
                        var url = await _firebaseService.UploadFileAsync(file, $"comments/{comment.Id}");
                        mediaUrls.Add(url);
                    }
                }
            }

            comment.MediaUrls = mediaUrls;

            if (!_commentRepository.UpdateComment(comment)) return StatusCode(500, "A problem occurred.");

            var createdCommentDto = _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, createdCommentDto);
        }

        // PUT: api/v1/comment/updateComment/{id}
        [HttpPut("updateComment/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateComment(string id, [FromBody] CommentDto commentDto)
        {
            if (commentDto == null || commentDto.Id != id) return BadRequest("Comment is null or ID mismatch.");

            if (!_commentRepository.CommentExists(id)) return NotFound();

            var comment = _mapper.Map<Comment>(commentDto);

            if (!_commentRepository.UpdateComment(comment)) return StatusCode(500, "A problem occurred.");

            return NoContent();
        }

        // DELETE: api/v1/comment/deleteComment/{id}
        [HttpDelete("deleteComment/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteComment(string id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null) return NotFound();

            try
            {
                await _firebaseService.DeleteFolderAsync($"comments/{id}/");

                if (!_commentRepository.DeleteComment(id)) return StatusCode(500, "A problem occurred.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return NoContent();
        }
    }
}
