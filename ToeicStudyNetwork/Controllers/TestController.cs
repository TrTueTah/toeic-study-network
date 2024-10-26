using Microsoft.AspNetCore.Mvc;

namespace ToeicStudyNetwork.Controllers;

public class TestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
