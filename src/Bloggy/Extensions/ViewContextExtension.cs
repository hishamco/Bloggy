using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggy.Extensions
{
    public static class ViewContextExtensions
    {
        public static bool IsPost(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.Method == "POST";
        }
    }
}
