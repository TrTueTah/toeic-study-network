using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private readonly HttpClient _httpClient;
        public PersonalController(ILogger<PersonalController> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var token = Request.Cookies["token"];
            var user = new UserModel();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                user.ImageUrl = Request.Cookies["userImage"];
                user.Email = Request.Cookies["email"];
                user.Username = Request.Cookies["given_name"];
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfor([FromBody] UpdateUser user)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = Request.Cookies["userId"];
                object data;
                if (user.Username == null && user.ImageUrl != null)
                {
                    data = new { ImageUrl = user.ImageUrl };
                }
                else if (user.Username != null && user.ImageUrl == null)
                {
                    data = new { Username = user.Username };
                }
                else
                {
                    data = new { Username = user.Username, ImageUrl = user.ImageUrl };
                }
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"http://localhost:5112/api/v1/users/updateUser/{userId}", content);
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
        public class UpdateUser
        {
            [AllowNull]
            public string Username { get; set; }
            [AllowNull]
            public string ImageUrl { get; set; }
        }
    }
}