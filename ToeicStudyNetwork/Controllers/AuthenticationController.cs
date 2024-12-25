using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ToeicStudyNetwork.ViewModels;
using ToeicStudyNetwork.ViewModels.Authentication;

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
        public async Task<IActionResult> SignIn(SignInViewModel model)
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
                    SetAuthCookies(token);

                    if (HasAdminRole(token)) 
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage ?? "Yêu cầu không hợp lệ.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Vui lòng thử lại.");
            }
            
            return View(model);
        }
        
       [HttpPost]
public async Task<IActionResult> SignUp(SignUpViewModel model)
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
            SetAuthCookies(token);
            return RedirectToAction("Index", "Home");
        }
    }

    if (response.StatusCode == HttpStatusCode.BadRequest)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        try
        {
            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(errorContent);
            foreach (var error in errors)
            {
                foreach (var errorMessage in error.Value)
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Yêu cầu không hợp lệ.");
        }
    }
    else if (response.StatusCode == HttpStatusCode.Conflict)
    {
        ModelState.AddModelError(string.Empty, "Email đã tồn tại.");
    }
    else if (response.StatusCode == HttpStatusCode.Unauthorized)
    {
        ModelState.AddModelError(string.Empty, "Không được phép truy cập. Vui lòng thử lại.");
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Vui lòng thử lại.");
    }

    return View(model);
}


        public IActionResult SignIn()
        {
            if (IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult SignUp()
        {
            if (IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
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
        
        private void SetAuthCookies(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            Response.Cookies.Append("token", token);
            Response.Cookies.Append("userId", jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
            Response.Cookies.Append("role", jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value);
            Response.Cookies.Append("email", jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
            Response.Cookies.Append("given_name", jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value);
            Response.Cookies.Append("userImage", jwtToken.Claims.FirstOrDefault(c => c.Type == "userImage")?.Value);
        }
        
        private bool IsAuthenticated()
        {
            var token = Request.Cookies["token"];
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }
        
        private bool HasAdminRole(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            return role == "admin";
        }
    }
}
