using Bloggy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloggy.Services
{
    public class BlogService : IBlogService
    {
        private readonly BloggingContext _db;

        public BlogService(BloggingContext db)
        {
            _db = db;
        }

        public Post GetPost(string slug)
        {
            return _db.Posts.Include(p => p.Comments).SingleOrDefault(p => p.Slug == slug);
        }

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts;
        }

        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public IEnumerable<string> GetTags()
        {
            var tags = _db.Posts
                .SelectMany(p => p.Tags.Split(','))
                .Distinct()
                .OrderBy(t => t);

            return tags;
        }
    }
}
