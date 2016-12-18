using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bloggy.TagHelpers
{
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : TagHelper
    {
        public AlertType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var @class = "alert alert-";
            output.TagName = "div";

            switch (Type)
            {
                case AlertType.Error:
                    @class += "danger";
                    break;
                case AlertType.Information:
                    @class += "info";
                    break;
                case AlertType.Success:
                    @class += "success";
                    break;
                case AlertType.Warning:
                    @class += "warning";
                    break;
            }
            output.Attributes.Add("class", @class);
            output.Attributes.Add("role", "alert");
        }
    }
}
