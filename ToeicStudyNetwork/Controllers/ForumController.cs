using Microsoft.AspNetCore.Mvc;
using ToeicStudyNetwork.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToeicStudyNetwork.Dtos;
using ToeicStudyNetwork.ViewModels.Forum;

namespace ToeicStudyNetwork.Controllers
{
    [Route("[controller]")]
    public class ForumController : Controller
    {
        private readonly HttpClient _httpClient;

        public ForumController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex, int pageSize = 5)
        {
            var response = await FetchAllPosts(pageIndex + 1, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_PostListPartial", response.Results);
            }

            var posts = response.Results.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                MediaUrls = p.MediaUrls,
                CreatedAt = p.CreatedAt,
                Likes = p.Likes,
                Comments = p.Comments,
                UserId = p.UserId,
                UserName = p.UserName,
                UserImageUrl = p.UserImageUrl
            }).ToList();

            ViewBag.UserImageUrl = Request.Cookies["userImage"];
            ViewBag.UserName = Request.Cookies["given_name"];
            return View(posts);
        }
        
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> PostDetail([FromRoute] string postId)
        {
            var postData = await FetchPostById(postId);
            
           
            ViewBag.UserImageUrl = Request.Cookies["userImage"];
            ViewBag.UserName = Request.Cookies["given_name"];
            
            return View("PostDetail", postData);
        }
        
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] string content, [FromForm] List<IFormFile> files)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Content cannot be empty.");
            }

            if (files.Count == 0)
            {
                return BadRequest("At least one file is required.");
            }

            var userId = Request.Cookies["userId"];

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(userId), "UserId");
            formData.Add(new StringContent(content), "Content");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formData.Add(fileContent, "MediaFiles", file.FileName); 
                }
            }

            var response = await _httpClient.PostAsync("http://localhost:5112/api/v1/post/createPost", formData);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error: {errorMessage}");
            }

            return RedirectToAction("Index");
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment([FromForm] string content, [FromForm] List<IFormFile> files, [FromForm] string postId)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Content cannot be empty.");
            }
            
            var userId = Request.Cookies["userId"];

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(userId), "UserId");
            formData.Add(new StringContent(content), "Content");
            formData.Add(new StringContent(postId), "PostId");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formData.Add(fileContent, "MediaFiles", file.FileName);
                }
            }

            var response = await _httpClient.PostAsync("http://localhost:5112/api/v1/comment/createComment", formData);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error: {errorMessage}");
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost("ToggleLike")]
        public async Task<IActionResult> ToggleLike([FromBody] ToggleLikeRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.PostId))
            {
                return BadRequest("Invalid PostId");
            }

            var token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            var userEmail = Request.Cookies["email"];
            var userIdResponse = await _httpClient.GetAsync($"http://localhost:5112/api/v1/users/getUserIdByEmail/{userEmail}");
            userIdResponse.EnsureSuccessStatusCode();
            var userId = await userIdResponse.Content.ReadAsStringAsync();

            var likeData = new { UserId = userId, PostId = request.PostId };

            var response = await _httpClient.PostAsync("http://localhost:5112/api/v1/like/toggleLike",
                new StringContent(JsonConvert.SerializeObject(likeData), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error: {errorMessage}");
            }

            return Ok(new { Success = true });
        }
        
        [NonAction]
        private async Task<PostResponse> FetchAllPosts(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/post/getAllPosts?page={pageIndex}&limit={pageSize}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<PostResponse>(responseData);

            return posts;
        }
        
        [NonAction]
        private async Task<PostViewModel> FetchPostById(string postId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5112/api/v1/post/getPostById/{postId}");

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<PostViewModel>(responseData);
            return post;
        }
    }
}
