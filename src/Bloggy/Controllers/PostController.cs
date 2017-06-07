using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Bloggy.Controllers
{
    public class PostController : BaseController
    {
        public PostController(BloggingContext db)
            :base(db)
        {

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
            Db.Posts.Add(post);
            Db.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        public IActionResult Edit(int id)
        {
            var post = Db.Posts.SingleOrDefault(p => p.Id == id);

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
            Db.Entry(post).State = EntityState.Modified;
            Db.SaveChanges();

            return RedirectToAction("Details", "Blog", new { slug = post.Slug });
        }
    }
}
