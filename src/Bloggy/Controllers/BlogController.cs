using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Bloggy.Controllers
{
    public class BlogController : BaseController
    {
        private Blog _blog;

        public BlogController(BloggingContext db, IOptions<AppSettings> appSettings)
            :base(db)
        {
            _blog = appSettings.Value.Blog;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var postsNo = _blog.PostsPerPage;
            var posts = Db.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Take(postsNo);

            return View(posts);
        }

        [Route("/{slug}")]
        public IActionResult Details(string slug)
        {
            var post = Db.Posts.Include(p => p.Comments)
                .SingleOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            return View(post);
        }

        [Route("/{slug}")]
        [HttpPost]
        public IActionResult Details(string slug, Comment comment)
        {
            var post = Db.Posts
                .Single(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            if (ModelState.IsValid)
            {
                comment.PostId = post.Id;
                comment.PublishedAt = DateTime.UtcNow;
                comment.Content = comment.Content.Replace(Environment.NewLine, "<br/>");
                Db.Comments.Add(comment);
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
            var posts = Db.Posts;

            return View(posts);
        }

        [Route("/tags")]
        public IActionResult Tags()
        {
            var tags = Db.Posts
                .SelectMany(p => p.Tags.Split(','))
                .Distinct()
                .OrderBy(t => t);

            return View(tags);
        }

        [Route("/about")]
        public IActionResult About() => View();

        [Route("/contact")]
        public IActionResult Contact() => View();

        [Route("/error")]
        public IActionResult Error() => View();
    }
}
