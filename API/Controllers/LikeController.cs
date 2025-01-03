﻿using API.Interfaces;
using API.Models;
using API.Dtos.LikeDto;
using API.Dtos.UserDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public LikeController(ILikeRepository likeRepository, IMapper mapper, IUserRepository userRepository, IPostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        // POST: api/v1/like/toggleLike
        [HttpPost("toggleLike")]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult ToggleLike([FromBody] LikeDto likeDto)
        {
            if (likeDto == null)
            {
                return BadRequest("Like data is null.");
            }

            if (!_userRepository.UserExists(likeDto.UserId))
            {
                return NotFound("User not found.");
            }

            if (!_postRepository.PostExists(likeDto.PostId))
            {
                return NotFound("Post not found.");
            }

            bool isLiked = _likeRepository.UserHasLikedPost(likeDto.PostId, likeDto.UserId);

            if (isLiked)
            {
                if (!_likeRepository.DeleteLike(likeDto.PostId, likeDto.UserId))
                {
                    return StatusCode(500, "A problem occurred while unliking the post.");
                }
                return Ok(new
                {
                    status = "deleted",
                    isLiked = false,
                    postId = likeDto.PostId,
                    userId = likeDto.UserId
                });
            }
            else
            {
                var like = _mapper.Map<Like>(likeDto);

                if (!_likeRepository.CreateLike(like))
                {
                    return StatusCode(500, "A problem occurred while liking the post.");
                }
                
                return Ok(new
                {
                    status = "created",
                    isLiked = true,
                    postId = likeDto.PostId,
                    userId = likeDto.UserId
                });
            }
        }

        // GET: api/v1/like/getLikedUsers/{postId}
        [HttpGet("getLikedUsers/{postId}")]
        [ProducesResponseType(typeof(ICollection<UserNameDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ICollection<UserNameDto>> GetLikedUsers(string postId)
        {
            if (!_postRepository.PostExists(postId))
            {
                return NotFound("Post not found.");
            }

            var likes = _likeRepository.GetLikesByPostId(postId);
            if (likes == null || likes.Count == 0)
            {
                return NotFound("No likes found for this post.");
            }

            var userDtos = likes
                .Select(like => _userRepository.GetUserById(like.UserId))
                .Where(user => user != null)
                .Select(user => _mapper.Map<UserNameDto>(user))
                .ToList();

            if (userDtos.Count == 0)
            {
                return NotFound("No users found for this post.");
            }

            return Ok(userDtos);
        }
        [HttpGet("getLikesByPostId/{postId}")]
        public ActionResult GetLikesByPostId(string postId)
        {
            var likes = _likeRepository.GetLikesByPostId(postId);
            if (likes == null)
            {
                return NotFound("No likes found for this post.");
            }
            var likeResponse = new List<LikeResponseDto>();
            foreach (var like in likes)
            {
                likeResponse.Add(new LikeResponseDto
                {
                    Id = like.Id,
                    UserId = like.UserId,
                    PostId = like.PostId,
                    LikedAt = like.LikedAt,
                    Username = _userRepository.GetUserNameById(like.UserId),
                });
            }
            return Ok(likeResponse);
        }
    }
}
