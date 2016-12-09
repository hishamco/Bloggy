using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
