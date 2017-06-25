using Bloggy.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bloggy
{
    public class PostModel : PageModel
    {
        private readonly BloggingContext _db;
        private Blog _blog;

        public PostModel(BloggingContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _blog = appSettings.Value.Blog;
        }

        public Post Post { get; private set; }

        public Blog Blog => _blog;

        [BindProperty]
        public Comment Comment { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public bool ShowSuccessMessage => !string.IsNullOrEmpty(SuccessMessage);

        [TempData]
        public string ErrorMessage { get; set; }

        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public IActionResult OnGet(string slug)
        {
            Post = _db.Posts.Include(p => p.Comments)
                .SingleOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            if (Post != null)
            {
                return Page();
            }

            return RedirectToPage("/Error");
        }

        public async Task<IActionResult> OnPostAddCommentAsync(string slug)
        {
            Post = _db.Posts.Include(p => p.Comments)
                .Single(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Unable to add comment";
                return Page();
            }

            Comment.PostId = Post.Id;
            Comment.PublishedAt = DateTime.UtcNow;
            Comment.Content = Comment.Content.Replace(Environment.NewLine, "<br/>");
            _db.Comments.Add(Comment);
            await _db.SaveChangesAsync();
            SuccessMessage = "Your comment has been added";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(int id)
        {
            var comment = _db.Comments.Include(c => c.Post)
                .SingleOrDefault(c => c.Id == id);

            if (comment != null)
            {
                _db.Comments.Remove(comment);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}