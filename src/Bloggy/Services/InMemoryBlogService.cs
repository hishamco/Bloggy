using System;
using System.Collections.Generic;
using Bloggy.Models;

namespace Bloggy.Services
{
    public class InMemoryBlogService : IBlogService
    {
        private readonly BloggingDbContext _db;

        public InMemoryBlogService(BloggingDbContext db)
        {
            _db = db;
            AddSeedData();
        }

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts;
        }

        private void AddSeedData()
        {
            _db.Posts.AddRange(
                new Post
                {
                    Id = 1,
                    Title = "What is Lorem Ipsum?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 2)
                },
                new Post
                {
                    Id = 2,
                    Title = "Where does it come from?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 4)
                },
                new Post
                {
                    Id = 3,
                    Title = "Why do we use it?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 6)
                },
                new Post
                {
                    Id = 4,
                    Title = "What is Lorem Ipsum?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 8)
                },
                new Post
                {
                    Id = 5,
                    Title = "Where does it come from?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 10)
                },
                new Post
                {
                    Id = 6,
                    Title = "Why do we use it?",
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    LastModified = new DateTime(2016, 12, 10),
                    IsPublished = false
                }
            );

            _db.SaveChanges();
        }
    }
}
