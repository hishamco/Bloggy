using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bloggy.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(BloggingContext db)
            :base(db)
        {

        }

        public IActionResult Delete(int id)
        {
            var comment = Db.Comments.Include(c => c.Post)
                .SingleOrDefault(c => c.Id == id);

            if (comment != null)
            {
                Db.Comments.Remove(comment);
                Db.SaveChanges();
            }

            return RedirectToAction("Details", "Blog", new { slug = comment.Post.Slug});
        }
    }
}
