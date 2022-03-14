using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : BaseController
    {        
        public AuthController(IRepositoryManager provider, IConfiguration configuration) : base(provider, configuration)
        {
        }
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(Member obj)
        {
            var result = provider.Member.Add(obj);
            //string[] message = { "Email hoặc tên đăng nhập đã có người sử dụng.", "Có lỗi xảy ra, vui lòng thử lại", "Đăng kí thành công" };
            string[] message = { "Email hoặc tên đăng nhập đã có người sử dụng.", "Đăng kí thành công" };
            //TempData["msg"] = message[result + 1];
            TempData["msg"] = message[result];
            return Redirect("/auth/login");
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Member obj, string returnUrl)
        {
            Member member = provider.Member.Login(new Member { Username = obj.Username, Password = obj.Password });
            if (member != null)
            {
                if (member.IsBanned)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản của bạn đã bị khóa");
                    return View(obj);

                }
                else
                {
                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, member.Username),
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                    new Claim(ClaimTypes.Email, member.Email)
                };

                    IEnumerable<Role> roles = provider.Role.GetRolesByMember(member.MemberId);
                    foreach (Role role in roles)
                    {
                        if (role.Checked)
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
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/member" : returnUrl);
                }
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
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            Member member = provider.Member.GetMemberByEmail(email.Trim());
            if (member != null) {
                if(member.IsBanned == false)
                {
                    member.Token = SiteHelper.CreateToken(32);
                    provider.Member.SaveTokenOfMember(member);

                    //Send email to reset password
                    IConfigurationSection section = configuration.GetSection("Email:Outlook");
                    SmtpClient client = new SmtpClient(section["host"], Convert.ToInt32(section["port"]))
                    {
                        Credentials = new NetworkCredential(section["address"], SiteHelper.DecryptString(section["password"])),
                        EnableSsl = true
                    };
                    MailAddress addressFrom = new MailAddress(section["address"]);
                    MailAddress addressTo = new MailAddress(email.Trim());
                    MailMessage message = new MailMessage(addressFrom, addressTo);
                    message.IsBodyHtml = true;
                    message.Body = $"Vui lòng click vào <a href=\"https://localhost:44389/auth/resetpassword/{member.Token}\">ĐÂY</a> để thiết lập lại mật khẩu của bạn. ";
                    message.Subject = "CẬP NHẬT MẬT KHẨU";
                    client.SendCompleted += (s, e) =>
                    {                        
                        message.Dispose();
                        client.Dispose();
                    };
                    await client.SendMailAsync(message);
                    
                    TempData["msg"] = $"Email chứa liên kết thiết lập lại mật khẩu đã được gửi tới {email}. Vui lòng kiểm tra Inbox/Spam của email.";
                    return Redirect("/auth/login");
                }
                else
                {
                    TempData["msg"] = "Tài khoản này đã bị khóa";
                    return View();
                }                
            }
            TempData["msg"] = "Email không tồn tại";
            return View();
        }
        public IActionResult ResetPassword(string id)
        {
            Member member = provider.Member.GetMemberByToken(id);
            if(member != null)
            {
                return View();
            }
            TempData["msg"] = "Liên kết đã hết hạn hoặc không tồn tại";
            return Redirect("/auth/login");
        }
        [HttpPost]
        public IActionResult ResetPassword(string id, ResetPasswordViewModel obj)
        {
            if(obj.NewPassword == obj.RePassword)
            {
                obj.Token = id;
                provider.Member.ResetPassword(obj);                
                TempData["msg"] = "Đã thiết lập mật khẩu mới";
                return Redirect("/auth/login");
            }
            ModelState.AddModelError(string.Empty, "Các mật khẩu đã nhập không khớp. Hãy thử lại");
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel obj)
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Member member = provider.Member.GetMemberById(memberId);
            if (member != null)
            {
                member.Password = obj.OldPassword;
                bool paswordValid = provider.Member.CheckCurrentPassword(member);
                if (paswordValid && obj.NewPassword == obj.RePassword)
                {
                    member.Password = obj.NewPassword;
                    provider.Member.UpdatePassword(member);
                    TempData["msg"] = "Đã cập nhật mật khẩu mới";
                }
                else if (paswordValid)
                {
                    TempData["msg"] = "Các mật khẩu mới đã nhập không khớp. Hãy thử lại";
                }
                else
                {
                    TempData["msg"] = "Mật khẩu hiện tại không chính xác. Hãy thử lại";
                }
                return Redirect("/member");
            }
            return Redirect("/auth/login");

        }
    }
}
