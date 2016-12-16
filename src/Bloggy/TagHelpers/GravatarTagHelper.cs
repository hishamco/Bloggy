using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Cryptography;
using System.Text;

namespace Bloggy.TagHelpers
{
    [HtmlTargetElement("gravatar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class GravatarTagHelper : TagHelper
    {
        public string Email { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            var hash = ComputeHash(Email);
            output.Content.SetHtmlContent($"<img src=\"http://www.gravatar.com/avatar/{hash}\" />");
        }

        private string ComputeHash(string email)
        {
            var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(email);
            var hash = md5.ComputeHash(bytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
