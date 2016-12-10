using Bloggy.Models;
using Bloggy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Bloggy.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private IOptions<Blog> _blogOptions;

        public BlogController(IBlogService blogService, IOptions<Blog> blogOptions)
        {
            _blogService = blogService;
            _blogOptions = blogOptions;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var posts = GetLatestPosts();

            return View(posts);
        }

        private IEnumerable<Post> GetLatestPosts()
        {
            var postsNo = _blogOptions.Value.PostsPerPage;

            return _blogService.GetPosts()
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt.Value)
                .Take(postsNo);
        }
    }
}
