using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        SiteProvider provider;
        public AuthController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Member obj)
        {
            var result = provider.Member.Add(obj);
            string[] message = { "Tên đăng nhập đã có người sử dụng.","Có lỗi xảy ra, vui lòng thử lại", "Đăng kí thành công" };
            TempData["msg"] = message[result + 1];
            return Redirect("/auth/login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(Member obj, string returnUrl)
        {
            Member member = provider.Member.Login(new Member {Username = obj.Username, Password = obj.Password });
            if(member != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, member.Username),                    
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                    new Claim(ClaimTypes.Email, member.Email)                    
                };

                IEnumerable<Role> roles = provider.Role.GetRolesByMember(member.MemberId);
                if(roles != null)
                {
                    foreach (Role role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                    }
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = obj.Remember,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                };
                await HttpContext.SignInAsync(principal, properties);
                return Redirect(string.IsNullOrEmpty(returnUrl) ? "/auth" : returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(obj);
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/auth/login" : returnUrl);
        }
    }
}
