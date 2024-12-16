using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllowanceManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace AllowanceManagement.Controllers
{
    public class AccountController : Controller
    {
        // Fixed usernames and passwords for easy change
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "admin", "admin" },
            { "user", "user" }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Account model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_users.TryGetValue(model.Username, out var password) && model.Password == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Employee");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Employee");
        }
    }
}
