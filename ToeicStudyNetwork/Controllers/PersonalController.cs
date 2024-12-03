using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToeicStudyNetwork.Models;

namespace ToeicStudyNetwork.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;

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
    }
}