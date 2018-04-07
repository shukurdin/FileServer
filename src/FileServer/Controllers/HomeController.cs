using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}