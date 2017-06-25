using Bloggy.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bloggy.Pages.Account.Posts
{
    public class EditPostModel : PageModel
    {
        private readonly BloggingContext _db;

        public EditPostModel(BloggingContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Post Post { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = _db.Posts.SingleOrDefault(p => p.Id == id);

            if (Post != null)
            {
                return Page();
            }

            return RedirectToPage("/Error");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Post.IsPublished)
            {
                Post.PublishedAt = DateTime.Now;
            }
            Post.LastModified = DateTime.Now;
            _db.Entry(Post).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToPage("/Post", new { slug = Post.Slug});
        }
    }
}