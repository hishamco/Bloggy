using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bloggy.Controllers
{
    public class CommentController : Controller
    {
        private BloggingContext _db;

        public CommentController(BloggingContext db)
        {
            _db = db;
        }

        public IActionResult Delete(int id)
        {
            var comment = _db.Comments.Include(c => c.Post)
                .SingleOrDefault(c => c.Id == id);

            if (comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }

            return RedirectToAction("Details", "Blog", new { slug = comment.Post.Slug});
        }
    }
}
