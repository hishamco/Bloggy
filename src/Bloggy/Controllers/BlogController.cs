using Bloggy.Models;
using Bloggy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloggy.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private Blog _blog;

        public BlogController(IBlogService blogService, IOptions<AppSettings> appSettings)
        {
            _blogService = blogService;
            _blog = appSettings.Value.Blog;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var posts = GetLatestPosts();

            return View(posts);
        }

        [Route("/{slug}")]
        public IActionResult Details(string slug)
        {
            var post = _blogService.GetPost(slug);

            return View(post);
        }

        [Route("/{slug}")]
        [HttpPost]
        public IActionResult Details(string slug, Comment comment)
        {
            var post = _blogService.GetPost(slug);

            if (ModelState.IsValid)
            {
                comment.PostId = post.Id;
                comment.PublishedAt = DateTime.UtcNow;
                comment.Content = comment.Content.Replace(Environment.NewLine, "<br/>");
                _blogService.AddComment(comment);
                TempData["Message"] = "Your comment has been added";
            }
            else
            {
                TempData["Message"] = "Unable to add comment";
            }

            return View(post);
        }

        [Route("/archive")]
        public IActionResult Archive()
        {
            var posts = _blogService.GetPosts();

            return View(posts);
        }

        [Route("/tags")]
        public IActionResult Tags()
        {
            var tags = _blogService.GetTags();

            return View(tags);
        }

        [Route("/about")]
        public IActionResult About() => View();

        [Route("/contact")]
        public IActionResult Contact() => View();

        [Route("/error")]
        public IActionResult Error() => View();

        private IEnumerable<Post> GetLatestPosts()
        {
            var postsNo = _blog.PostsPerPage;

            return _blogService.GetPosts()
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Take(postsNo);
        }
    }
}
