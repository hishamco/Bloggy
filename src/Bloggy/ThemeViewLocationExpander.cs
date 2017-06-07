using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Bloggy
{
    public class ThemeViewLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "theme";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            context.Values.TryGetValue(ValueKey, out string theme);

            if (!string.IsNullOrEmpty(theme))
            {
                return ExpandViewLocationsCore(viewLocations, theme);
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var appSettings = context.ActionContext.HttpContext.RequestServices
                .GetService(typeof(IOptions<AppSettings>)) as IOptions<AppSettings>;

            context.Values[ValueKey] = appSettings.Value.Blog.Theme;
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, string theme)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Insert(7, $"Themes/{theme}/");
            }
        }
    }
}
