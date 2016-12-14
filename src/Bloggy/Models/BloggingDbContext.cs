using Microsoft.EntityFrameworkCore;

namespace Bloggy.Models
{
    public class BloggingDbContext : DbContext
    {
        public BloggingDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
