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
        private BlogService _blogService;
        private IOptions<Blog> _blogOptions;

        public BlogController(BlogService blogService, IOptions<Blog> blogOptions)
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
                _blogService.AddComment(comment);
            }

            return View(post);
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
