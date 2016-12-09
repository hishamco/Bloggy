using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Controllers
{
    public class BlogController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
