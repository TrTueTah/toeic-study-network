using API.Interfaces;
using API.Models;
using API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly FirebaseService _firebaseService;

        public UserController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, FirebaseService firebaseService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _firebaseService = firebaseService;
        }

        // GET: api/v1/users/{userId}
        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<UserDto> GetUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("getUserIdByEmail/{email}")]
        public ActionResult<string> GetUserIdByEmail(string email)
        {
            var userId = _userRepository.GetUserIdByEmail(email);
            if (userId == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userId);
        }
        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromForm] UpdateUserDto userDto)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (userDto.Username != null)
            {
                user.Username = userDto.Username;
            }
            if (userDto.ImageFile != null)
            {
                var url = await _firebaseService.UploadFileAsync(userDto.ImageFile, $"userAvatar/{id}");
                user.ImageUrl = url;
            }
            var updatedUser = _userRepository.UpdateUser(user);
            if (updatedUser.Username != null)
            {
                user.Username = updatedUser.Username;
            }
            if (updatedUser.ImageUrl != null)
            {
                user.ImageUrl = updatedUser.ImageUrl;
            }
            var token = _tokenService.CreateToken(user, "AccessToken");
            return Ok(token);
        }
    }
}