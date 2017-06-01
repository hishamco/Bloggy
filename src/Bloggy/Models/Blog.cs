using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Models
{
    public class Blog
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int PostsPerPage { get; set; }

        public bool AllowComments { get; set; }
    }
}
