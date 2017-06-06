using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Bloggy.Controllers
{
    public class PostController : Controller
    {
        private BloggingContext _db;

        public PostController(BloggingContext db)
        {
            _db = db;
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            if (post.IsPublished)
            {
                post.PublishedAt = DateTime.Now;
            }
            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        public IActionResult Edit(int id)
        {
            var post = _db.Posts.SingleOrDefault(p => p.Id == id);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            if (post.IsPublished)
            {
                post.PublishedAt = DateTime.Now;
            }
            post.LastModified = DateTime.Now;
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Details", "Blog", new { slug = post.Slug });
        }
    }
}
