using System;
using System.Collections.Generic;
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
        public PersonalController(ILogger<PersonalController> logger)
        {
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

                user.ImageUrl = jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value;
                user.Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                user.Username = jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameRequest user)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

                var response = await _httpClient.PostAsync($"http://localhost:5112/api/v1/users/updateUser/fcbe22b6-4b1e-495e-bdc6-edfce37258ed", new StringContent(JsonConvert.SerializeObject(user.Username), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
            return RedirectToAction("Index");
        }
        public class UpdateUsernameRequest
        {
            public string Username { get; set; }
        }
    }
}