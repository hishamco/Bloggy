namespace Bloggy.Models
{
    public class Blog
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int PostsPerPage { get; set; }

        public bool AllowComments { get; set; }

        public int DaysToComment { get; set; }

        public string Theme { get; set; }
    }
}
