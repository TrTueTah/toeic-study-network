using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        private async Task<List<PostModel>> FetchPostsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/post/getAllPosts");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostModel>>(responseString);
            
            return posts;
        }
        
    }
}