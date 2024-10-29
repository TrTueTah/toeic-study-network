using API.Interfaces;
using API.Models;
using API.Dtos.Post; // Thêm namespace cho DTOs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Dtos.Post;
using AutoMapper; // Thêm AutoMapper

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // GET: api/v1/post/getAllPosts
        [HttpGet("getAllPosts")]
        [ProducesResponseType(typeof(ICollection<PostDto>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<ICollection<PostDto>> GetPosts()
        {
            var posts = _postRepository.GetAllPosts();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return Ok(postDtos);
        }

        // GET: api/v1/post/getPostByPostId/{id}
        [HttpGet("getPostByPostId/{id}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<PostDto> GetPost(int id)
        {
            var post = _postRepository.GetPostByPostId(id);
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
        public ActionResult<PostDto> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            if (createPostDto == null)
            {
                return BadRequest("Post is null.");
            }

            var post = _mapper.Map<Post>(createPostDto);
            
            post.CreatedAt = DateTime.Now;
            
            // List<string> mediaUrls = new List<string>();
            //
            // if (createPostDto.MediaUrls != null && createPostDto.MediaUrls.Count > 0)
            // {
            //     mediaUrls.AddRange(createPostDto.MediaUrls);
            // }

            if (!_postRepository.CreatePost(post))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPostDto = _mapper.Map<PostDto>(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, createdPostDto);
        }

        // PUT: api/v1/post/updatePost/{id}
        [HttpPut("updatePost/{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(500)] // Internal Server Error
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
        public ActionResult DeletePost(int id)
        {
            if (!_postRepository.PostExists(id))
            {
                return NotFound();
            }

            if (!_postRepository.DeletePost(id))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
