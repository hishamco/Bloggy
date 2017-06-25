using Bloggy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bloggy
{
    public class TagsModel : PageModel
    {
        private readonly BloggingContext _db;

        public TagsModel(BloggingContext db)
        {
            _db = db;
        }

        public IList<string> Tags { get; set; }

        public async Task OnGetAsync()
        {
            Tags = await _db.Posts
                .SelectMany(p => p.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();
        }

        public IList<Post> GetPosts(string tag)
        {
            return _db.Posts
                .Where(p => p.Tags.Contains(tag))
                .OrderBy(p => p.PublishedAt)
                .ToList();
        }
    }
}