using Bloggy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bloggy.Pages.Account
{
    public class LoginModel : PageModel
    {
        private Credential _credential;

        public LoginModel(IOptions<AppSettings> appSettings)
        {
            _credential = appSettings.Value.Credential;
        }

        [BindProperty]
        public Bloggy.Models.User AdminUser { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (_credential.PasswordFormat != PasswordFormat.SHA1)
                {
                    throw new NotSupportedException();
                }

                if (AdminUser.Name == _credential.User.Name && GetHashedPassword(AdminUser.Password).Equals(_credential.User.Password, StringComparison.OrdinalIgnoreCase))
                {
                    var adminUser = new ClaimsPrincipal(
                        new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _credential.User.Name) },
                        CookieAuthenticationDefaults.AuthenticationScheme));
                    await PageContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminUser);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }

        private string GetHashedPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA1.Create().ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return LocalRedirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToPage("/Index");
        //    }
        //}
    }
}