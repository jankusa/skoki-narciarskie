using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace MvcSkoki.Controllers
{

    public class IOController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";

        // Hardcoded credentials for testing purposes
        private const string login = "admin";
        private const string passw = "admin";

        [HttpGet]
        public IActionResult Logowanie()
        {
            HttpContext.Session.Remove(SessionKeyLoggedIn);
            return View();
        }

        [HttpPost]
        public IActionResult Logowanie(string username, string password)
        {
            HttpContext.Session.Remove(SessionKeyLoggedIn);
            if (username == login && passw == password)
            {
                HttpContext.Session.SetString(SessionKeyLoggedIn, "true");
                return RedirectToAction("Index", "Home");;
            }
            Console.WriteLine(username);
            Console.WriteLine(password);
            ViewBag.ErrorMessage = "Invalid login or password.";
            return View();
        }

        // [HttpGet]
        // public IActionResult LoggedIn()
        // {
        //     if (HttpContext.Session.GetString(SessionKeyLoggedIn) == "true")
        //     {
        //         return View();
        //     }
        //     return RedirectToAction("Logowanie");
        // }

        // [HttpPost]
        // public IActionResult Wyloguj()
        // {
        //     HttpContext.Session.Remove(SessionKeyLoggedIn);
        //     return RedirectToAction("Logowanie");
        // }
    }
}