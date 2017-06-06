using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bloggy.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsPublished { get; set; }

        public DateTime PublishedAt { get; set; }

        [Required]
        public string Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
