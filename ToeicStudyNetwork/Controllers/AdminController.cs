using Microsoft.AspNetCore.Mvc;

namespace ToeicStudyNetwork.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
