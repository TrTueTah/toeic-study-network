using Microsoft.AspNetCore.Mvc;

namespace ToeicStudyNetwork.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateTest()
    {
        return View();
    }
    
    public IActionResult UploadAssets()
    {
        return View();
    }
}
