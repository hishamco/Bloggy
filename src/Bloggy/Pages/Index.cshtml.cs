using Bloggy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bloggy
{
    public class IndexModel : PageModel
    {
        private readonly BloggingContext _db;
        private Blog _blog;

        public IndexModel(BloggingContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _blog = appSettings.Value.Blog;
        }

        public IList<Post> Posts { get; private set; }

        public async Task OnGetAsync()
        {
            var postsNo = _blog.PostsPerPage;
            Posts = await _db.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Take(postsNo)
                .ToListAsync();
        }
    }
}