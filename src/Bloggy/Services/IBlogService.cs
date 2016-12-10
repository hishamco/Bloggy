using Bloggy.Models;
using System.Collections.Generic;

namespace Bloggy.Services
{
    public interface IBlogService
    {
        IEnumerable<Post> GetPosts();
    }
}
