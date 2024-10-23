using Microsoft.AspNetCore.Mvc;

namespace ToeicStudyNetwork.Controllers;

public class ForumController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
