using Microsoft.AspNetCore.Mvc;

namespace EmlakSon.WebUI.Controllers;

public class PropertiesController : Controller
{
    // Search & Filter View
    public IActionResult Index()
    {
        return View();
    }

    // Property Details View
    public IActionResult Details(int id)
    {
        ViewBag.PropertyId = id;
        return View();
    }
}
