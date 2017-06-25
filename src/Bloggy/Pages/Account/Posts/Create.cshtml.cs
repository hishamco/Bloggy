using Bloggy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Bloggy.Pages.Account.Posts
{
    public class CreatePostModel : PageModel
    {
        private readonly BloggingContext _db;

        public CreatePostModel(BloggingContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Post Post { get; set; }

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
            _db.Posts.Add(Post);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}