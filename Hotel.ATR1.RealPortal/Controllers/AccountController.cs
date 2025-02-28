using Hotel.ATR1.RealPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel.ATR1.RealPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Login, string Password)
        {
            AuthService service = new AuthService();
            var user = await service.ValidateUser(Login, Password);

            if(user!=null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, Login));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName));
                claims.Add(new Claim("username", Login));

                var claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
