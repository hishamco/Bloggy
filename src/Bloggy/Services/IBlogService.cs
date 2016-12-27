using Bloggy.Models;
using System.Collections.Generic;

namespace Bloggy.Services
{
    public interface IBlogService
    {
        Post GetPost(string slug);

        IEnumerable<Post> GetPosts();

        void AddComment(Comment comment);

        IEnumerable<string> GetTags();
    }
}
