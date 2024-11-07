using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthenticationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginData = new
            {
                Email = model.Email,
                Password = model.Password
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5112/api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var token = (string)responseObject.token;
                if (!string.IsNullOrEmpty(token))
                {
                    // Save the token to cookies
                    Response.Cookies.Append("token", token);

                    // Handle successful login (e.g., redirect to a dashboard)
                    return RedirectToAction("Index", "Home");
                }
            }

            // Handle login failure
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult SignIn()
        {
            var token = Request.Cookies["token"];
            Console.WriteLine(token);
            if (!string.IsNullOrEmpty(token))
            {
                if (IsTokenExpired(token))
                {
                    // Delete the token if it is expired
                    Response.Cookies.Delete("token");
                }
                else
                {
                    // Redirect to Home if the token is valid
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        private bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return true;

            var exp = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (exp == null)
                return true;

            var expDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;
            return expDate < DateTime.UtcNow;
        }
    }
}