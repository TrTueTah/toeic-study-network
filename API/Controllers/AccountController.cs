using API.Dtos.Account;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public AccountController(ITokenService tokenService, IUserRepository userRepository, IMapper mapper)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(UserLoginResponseDto), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.GetUserByEmail(loginDto.Email);

            if (user == null) return Unauthorized("Invalid Email!");

            string hashedPassword = _userRepository.HashPassword(loginDto.Password);
            if (user.Password != hashedPassword) return Unauthorized("Invalid Password!");

            // if () return Unauthorized("Username not found and/or password incorrect");
            var token = _tokenService.CreateToken(user, "AccessToken");
            var response = _mapper.Map<UserLoginResponseDto>(user);
            response.Token = token; // Set the generated token here

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error ", Error = e.Message });
        }
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserRegisterResponseDto), 200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUserByEmail = await _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return Conflict(new { Message = "Email is existed" });
            }

            var existingUserByUsername = _userRepository.GetUserByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return Conflict(new { Message = "Username is existed" });
            }

            var appUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = _userRepository.HashPassword(registerDto.Password),
            };

            var createdUser = _userRepository.CreateUser(appUser);

            if (createdUser != null)
            {
                var response = _mapper.Map<UserRegisterResponseDto>(createdUser);
                response.Token = _tokenService.CreateToken(createdUser, "AccessToken");
                return Ok(response);
            }
            else
            {
                return StatusCode(500, "There was an error creating the user");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Undefine Error", Error = e.Message });
        }
    }
}