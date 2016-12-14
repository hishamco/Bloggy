using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bloggy.TagHelpers
{
    [HtmlTargetElement("partial", Attributes = "name")]
    public class PartialTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;

        public PartialTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Name { get; set; }

        public object Model { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);

            output.TagName = null;

            var content = await _htmlHelper.PartialAsync(Name, Model);
            output.Content.SetHtmlContent(content);
        }
    }
}