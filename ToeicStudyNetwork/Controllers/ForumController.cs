using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using ToeicStudyNetwork.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net.Http.Headers;

namespace ToeicStudyNetwork.Controllers
{
    [Route("[controller]")]
    public class ForumController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ForumController> _logger;

        public ForumController(HttpClient httpClient, ILogger<ForumController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet("Newest")]
        public async Task<IActionResult> Index()
        {
            var forumModel = await FetchPostsAsync();
            forumModel.Posts.OrderByDescending(post => post.CreatedAt).ToList();
            forumModel.Type = "Newest";
            return View("Index", forumModel);
        }
        [HttpGet("Favourite")]
        public async Task<IActionResult> SortByLike()
        {
            var forumModel = await FetchPostsAsync();

            var handler = new JwtSecurityTokenHandler();
            var userId = Request.Cookies["userId"];

            var likedPosts = forumModel.Posts.Where(post => post.Likes.Any(like => like.UserId == userId)).ToList();
            forumModel.Posts = likedPosts;
            forumModel.Type = "Favourite";

            return View("Index", forumModel);
        }
        [NonAction]
        private async Task<ForumModel> FetchPostsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5112/api/v1/post/getAllPosts");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostModel>>(responseString);
            var user = new UserModel();

            foreach (var post in posts)
            {
                var comments = await _httpClient.GetAsync($"http://localhost:5112/api/v1/comment/getCommentsByPostId/{post.Id}");
                comments.EnsureSuccessStatusCode();
                var commentsString = await comments.Content.ReadAsStringAsync();
                post.Comments = JsonConvert.DeserializeObject<List<CommentModel>>(commentsString);

                var likes = await _httpClient.GetAsync($"http://localhost:5112/api/v1/like/getLikesByPostId/{post.Id}");
                likes.EnsureSuccessStatusCode();
                var likesString = await likes.Content.ReadAsStringAsync();
                post.Likes = JsonConvert.DeserializeObject<List<LikeModel>>(likesString);
            }

            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                user.ImageUrl = Request.Cookies["userImage"];
                user.Email = Request.Cookies["email"];
                user.Username = Request.Cookies["given_name"];
            }

            var forumModel = new ForumModel
            {
                Posts = posts,
                User = user
            };

            return forumModel;
        }
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] string content, [FromForm] List<IFormFile> files)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Content cannot be empty.");
            }

            if (files == null || files.Count == 0)
            {
                return BadRequest("At least one file is required.");
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

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(userId), "UserId");
            formData.Add(new StringContent(content), "Content");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formData.Add(fileContent, "MediaFiles", file.FileName); // Use "MediaFiles" as the key
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

            if (files == null || files.Count == 0)
            {
                return BadRequest("At least one file is required.");
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
                    formData.Add(fileContent, "MediaFiles", file.FileName); // Use "MediaFiles" as the key
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

    }
    public class ToggleLikeRequest
    {
        public string PostId { get; set; }
    }
}