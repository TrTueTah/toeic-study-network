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

                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                    Response.Cookies.Append("userId", jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
                    Response.Cookies.Append("userImage", jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value);
                    Response.Cookies.Append("email", jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
                    Response.Cookies.Append("given_name", jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value);

                    // Handle successful login (e.g., redirect to a dashboard)
                    return RedirectToAction("Index", "Home");
                }
            }

            // Handle login failure
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
                return View(model);
            }
            var signUpData = new
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
            };
            var json = JsonConvert.SerializeObject(signUpData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5112/api/account/register", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var token = (string)responseObject.token;
                if (!string.IsNullOrEmpty(token))
                {
                    // Save the token to cookies
                    Response.Cookies.Append("token", token);
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                    Response.Cookies.Append("userId", jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
                    Response.Cookies.Append("userImage", jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value);
                    Response.Cookies.Append("email", jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
                    Response.Cookies.Append("given_name", jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value);

                    // Handle successful login (e.g., redirect to a dashboard)
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public IActionResult SignIn()
        {
            var token = Request.Cookies["token"];
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
            var token = Request.Cookies["token"];
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