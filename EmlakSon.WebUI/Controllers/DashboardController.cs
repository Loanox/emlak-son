using Microsoft.AspNetCore.Mvc;

namespace EmlakSon.WebUI.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MyProperties()
    {
        return View();
    }

    public IActionResult Favorites()
    {
        return View();
    }
}
