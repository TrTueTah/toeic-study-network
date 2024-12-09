using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Dtos.RequestDtos;
using ToeicStudyNetwork.Models;
using ToeicStudyNetwork.ViewModels;
using ToeicStudyNetwork.ViewModels.Personal;

namespace ToeicStudyNetwork.Controllers
{
    [Route("[controller]")]
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private readonly HttpClient _httpClient;
        public PersonalController(ILogger<PersonalController> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["token"];
            var user = new UserModel();
            var personalModel = new PersonalViewModel();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                user.ImageUrl = Request.Cookies["userImage"];
                user.Email = Request.Cookies["email"];
                user.Username = Request.Cookies["given_name"];

                var userId = Request.Cookies["userId"];

                var userResultsResponse = await _httpClient.GetAsync($"http://localhost:5112/api/v1/result/getUserResultByUserId/{userId}");
                userResultsResponse.EnsureSuccessStatusCode();
                var userResultsString = await userResultsResponse.Content.ReadAsStringAsync();
                var userResults = JsonConvert.DeserializeObject<List<UserResultModel>>(userResultsString);

                personalModel.User = user;
                personalModel.UserResults = userResults;
            }

            return View("Index", personalModel);
        }
        [HttpGet("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View("ChangePassword");
        }
        [HttpPost("ChangePasswordRequest")]
        public async Task<IActionResult> ChangePasswordRequest([FromBody] ChangePasswordViewModel changePasswordViewModel)
        {
            var userId = Request.Cookies["userId"];

            // Kiểm tra ràng buộc mật khẩu
            if (string.IsNullOrEmpty(changePasswordViewModel.CurrentPassword) ||
                string.IsNullOrEmpty(changePasswordViewModel.NewPassword) ||
                string.IsNullOrEmpty(changePasswordViewModel.ConfirmPassword))
            {
                return Json(new { success = false, message = "All fields are required." });
            }

            if (changePasswordViewModel.NewPassword != changePasswordViewModel.ConfirmPassword)
            {
                return Json(new { success = false, message = "The new password and confirmation password do not match." });
            }

            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if (!passwordRegex.IsMatch(changePasswordViewModel.NewPassword))
            {
                return Json(new { success = false, message = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character." });
            }

            var data = new
            {
                CurrentPassword = changePasswordViewModel.CurrentPassword,
                NewPassword = changePasswordViewModel.NewPassword
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"http://localhost:5112/api/account/changePassword/{userId}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Password changed successfully!" });
            }
            else
            {
                return Json(new { success = false, message = responseContent });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfor([FromForm] UpdateUserRequest user)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = Request.Cookies["userId"];
                using var formData = new MultipartFormDataContent();
                if (user.Username == null && user.ImageFile != null)
                {
                    formData.Add(new StreamContent(user.ImageFile.OpenReadStream()), "ImageFile", user.ImageFile.FileName);
                }
                else if (user.Username != null && user.ImageFile == null)
                {
                    formData.Add(new StringContent(user.Username), "Username");
                }
                else
                {
                    formData.Add(new StringContent(user.Username), "Username");
                    formData.Add(new StreamContent(user.ImageFile.OpenReadStream()), "ImageFile", user.ImageFile.FileName);
                }

                var response = await _httpClient.PutAsync($"http://localhost:5112/api/v1/users/updateUser/{userId}", formData);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseContent))
                {
                    // Save the token to cookies
                    Response.Cookies.Append("token", responseContent);
                    jwtToken = handler.ReadToken(responseContent) as JwtSecurityToken;
                    Response.Cookies.Append("userImage", jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value);
                    Response.Cookies.Append("email", jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
                    Response.Cookies.Append("given_name", jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value);
                }
            }
            return RedirectToAction("Index");
        }
    }
}