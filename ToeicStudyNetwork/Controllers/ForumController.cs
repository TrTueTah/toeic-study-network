using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace ToeicStudyNetwork.Controllers
{
    public class ForumController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ForumController> _logger;

        public ForumController(HttpClient httpClient, ILogger<ForumController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var posts = await FetchPostsAsync();
            return View(posts);
        }

        private async Task<ForumModel> FetchPostsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/post/getAllPosts");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostModel>>(responseString);
            var user = new UserModel();

            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                user.ImageUrl = jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value;
                user.Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                user.Username = jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
            }

            var forumModel = new ForumModel
            {
                Posts = posts,
                User = user
            };

            return forumModel;
        }
    }
}