using API.Dtos.Account;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    public AccountController(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }


    [HttpPost("login")]
    [ProducesResponseType(typeof(NewUserDto), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = _userRepository.GetUserByEmail(loginDto.Email);

        if (user == null) return Unauthorized("Invalid Email!");

        string hashedPassword = _userRepository.HashPassword(loginDto.Password);
        if (user.Password != hashedPassword) return Unauthorized("Invalid Password!");

        // if () return Unauthorized("Username not found and/or password incorrect");

        return Ok(
            new NewUserDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, "AccessToken")
            }
        );
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra Username đã tồn tại chưa
            var existingUserByUsername = _userRepository.GetUserByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return Conflict(new { Message = "Username is existed" });
            }

            // Kiểm tra Email đã tồn tại chưa
            var existingUserByEmail = _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return Conflict(new { Message = "Email is existed" });
            }

            // Tạo user mới
            var appUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = _userRepository.HashPassword(registerDto.Password),
            };

            var createdUser = _userRepository.CreateUser(appUser);

            if (createdUser != null)
            {
                return Ok(
                    new NewUserDto
                    {
                        Username = appUser.Username,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser, "AccessToken")
                    }
                );
            }
            else
            {
                return StatusCode(500, "Lỗi trong quá trình tạo user.");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Đã xảy ra lỗi không xác định.", Error = e.Message });
        }
    }
}