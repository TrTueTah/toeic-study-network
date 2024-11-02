using API.Interfaces;
using API.Models;
using API.Dtos.PostDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Services;
using AutoMapper;
using Azure.Storage.Blobs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly BlobService _blobService;
        public PostController(IPostRepository postRepository, IMapper mapper, BlobService blobService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _blobService = blobService;
        }

        // GET: api/v1/post/getAllPosts
        [HttpGet("getAllPosts")]
        [ProducesResponseType(typeof(ICollection<PostDto>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<ICollection<PostDto>> GetAllPosts()
        {
            var posts = _postRepository.GetAllPosts();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return Ok(postDtos);
        }

        // GET: api/v1/post/getPostById/{id}
        [HttpGet("getPostById/{id}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<PostDto> GetPost(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        // GET: api/v1/post/getPostsByUserId/{userId}
        [HttpGet("getPostsByUserId/{userId}")]
        [ProducesResponseType(typeof(ICollection<PostDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ICollection<PostDto>> GetPostsByUserId(string userId)
        {
            var posts = _postRepository.GetPostsByUserId(userId);
            if (posts == null || posts.Count == 0)
            {
                return NotFound("No posts found for this user.");
            }
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return Ok(postDtos);
        }

        // POST: api/v1/post/createPost
        [HttpPost("createPost")]
        [ProducesResponseType(typeof(PostDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PostDto>> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            if (createPostDto == null)
            {
                return BadRequest("Post is null.");
            }

            var post = _mapper.Map<Post>(createPostDto);
            post.CreatedAt = DateTime.Now;
            post.MediaUrls = new List<string>();
            
            if (!_postRepository.CreatePost(post))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
            ICollection<string> mediaUrls = new List<string>();
    
            if (createPostDto.MediaFiles != null && createPostDto.MediaFiles.Count > 0)
            {
                foreach (IFormFile file in createPostDto.MediaFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        var url = await _blobService.UploadFileAsync(file, $"posts/{post.Id}");
                        mediaUrls.Add(url);
                    }
                }
            }
            
            post.MediaUrls = mediaUrls;

            if (!_postRepository.UpdatePost(post))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPostDto = _mapper.Map<PostDto>(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, createdPostDto);
        }



        // PUT: api/v1/post/updatePost/{id}
        [HttpPut("updatePost/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (postDto == null || postDto.Id != id)
            {
                return BadRequest("Post is null or ID mismatch.");
            }

            if (!_postRepository.PostExists(id))
            {
                return NotFound();
            }

            var post = _mapper.Map<Post>(postDto);

            if (!_postRepository.UpdatePost(post))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

// DELETE: api/v1/post/deletePost/{id}
        [HttpDelete("deletePost/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            try
            {
                await _blobService.DeleteFolderAsync(post.Id.ToString());
                
                if (!_postRepository.DeletePost(id))
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
