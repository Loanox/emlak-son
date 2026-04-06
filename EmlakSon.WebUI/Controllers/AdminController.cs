using Microsoft.AspNetCore.Mvc;

namespace EmlakSon.WebUI.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Categories()
    {
        return View();
    }
}
