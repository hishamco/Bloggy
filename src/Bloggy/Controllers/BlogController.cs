using Bloggy.Models;
using Bloggy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bloggy.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var posts = GetLatestPosts();

            return View(posts);
        }

        private IEnumerable<Post> GetLatestPosts(int postsNo = 5)
        {
            return _blogService.GetPosts()
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt.Value)
                .Take(postsNo);
        }
    }
}
