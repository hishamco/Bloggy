using Bloggy.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bloggy.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private Credential _credential;

        public AccountController(IOptions<AppSettings> appSettings)
        {
            _credential = appSettings.Value.Credential;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (_credential.PasswordFormat != PasswordFormat.SHA1)
                {
                    throw new NotSupportedException();
                }

                if (user.Name == _credential.User.Name && GetHashedPassword(user.Password).Equals(_credential.User.Password, StringComparison.OrdinalIgnoreCase))
                {
                    var adminUser = new ClaimsPrincipal(
                        new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _credential.User.Name) },
                        CookieAuthenticationDefaults.AuthenticationScheme));
                    await ControllerContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminUser);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(user);
                }
            }

            return View(user);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(BlogController.Index), "Blog");
        }

        private string GetHashedPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA1.Create().ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BlogController.Index), "Blog");
            }
        }
    }
}
