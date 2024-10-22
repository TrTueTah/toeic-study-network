using Microsoft.AspNetCore.Mvc;

namespace ToeicStudyNetwork.Controllers;

public class AuthenticationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }
}
