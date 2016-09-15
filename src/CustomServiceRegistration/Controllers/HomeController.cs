using Microsoft.AspNetCore.Mvc;

namespace CustomServiceRegistration.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
