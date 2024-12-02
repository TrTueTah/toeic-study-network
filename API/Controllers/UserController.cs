using API.Interfaces;
using API.Models;
using API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
    }
}