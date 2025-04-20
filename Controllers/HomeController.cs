using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcSkoki.Models;
using Microsoft.AspNetCore.Http;

namespace MvcSkoki.Controllers;

public class HomeController : Controller
{
    private const string SessionKeyLoggedIn = "_LoggedIn";
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
