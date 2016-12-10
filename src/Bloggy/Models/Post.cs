using System;

namespace Bloggy.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public string Content { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string Tags { get; set; }
    }
}
