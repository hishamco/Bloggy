using Microsoft.EntityFrameworkCore;

namespace Bloggy.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
