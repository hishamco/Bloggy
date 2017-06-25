﻿using Bloggy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bloggy
{
    public class ArchiveModel : PageModel
    {
        private readonly BloggingContext _db;

        public ArchiveModel(BloggingContext db)
        {
            _db = db;
        }

        public IList<Tuple<string, IList<Post>>> GroupedPostsByMonth { get; private set; }

        public async Task OnGetAsync()
        {
            var notPublished = DateTime.MinValue.ToString("MMMM yyyy");


            GroupedPostsByMonth = await _db.Posts.GroupBy(p => p.PublishedAt.ToString("MMMM yyyy"),
                (k, g) => Tuple.Create<string, IList<Post>>(k == notPublished ? "Not Published" : k, g.ToList())).ToListAsync();
        }
    }
}