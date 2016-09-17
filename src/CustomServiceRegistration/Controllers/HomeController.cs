using Microsoft.AspNetCore.Mvc;

namespace CustomServiceRegistration.Controllers
{
    public class HomeController : Controller
    {
        // Just return the index page to use angular as a client
        public IActionResult Index()
        {
            return View("index");
        }
    }
}
