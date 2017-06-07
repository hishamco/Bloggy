using Microsoft.AspNetCore.Mvc;
using Bloggy.Models;

namespace Bloggy.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(BloggingContext db)
        {
            Db = db;
        }

        public BloggingContext Db { get; }
    }
}
