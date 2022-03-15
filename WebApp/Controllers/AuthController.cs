using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly IMailService mailService;
        public AuthController(IRepositoryManager provider, IConfiguration configuration, IMailService mailService) : base(provider, configuration)
        {
            this.mailService = mailService;
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
        public IActionResult Register(RegisterModel obj)
        {
            if (!ModelState.IsValid)
                return View();
            Member emailExist = provider.Member.GetMemberByEmail(obj.Email);
            if (emailExist != null)
            {
                ModelState.AddModelError(nameof(obj.Email), "Email này đã được sử dụng");
                return View();
            }
            Member usernameExist = provider.Member.GetMemberByUsername(obj.Username);
            if (usernameExist != null)
            {
                ModelState.AddModelError(nameof(obj.Username), "Tên đăng nhập này đã được sử dụng");
                return View();
            }
            var result = provider.Member.Add(new Member
            {
                Username = obj.Username,
                Email = obj.Email,
                Password = obj.Password,
                Gender = obj.Gender
            });
            if (result > 0)
            {
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Đăng kí thành công"
                });
            }
            else
            {
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau"
                });

            }
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
        public async Task<IActionResult> Login(LoginModel obj, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View();
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
                    PushNotification(new NotificationOption
                    {
                        Type = "success",
                        Message = "Đăng nhập thành công"
                    });
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
            PushNotification(new NotificationOption
            {
                Type = "success",
                Message = "Đăng xuất thành công"
            });
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
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel obj)
        {
            if (!ModelState.IsValid)
                return View();
            Member member = provider.Member.GetMemberByEmail(obj.Email.Trim());
            if (member != null)
            {
                if (member.IsBanned == false)
                {
                    member.Token = SiteHelper.CreateToken(32);
                    provider.Member.SaveTokenOfMember(member);
                    //Send email to reset password
                    bool result = await mailService.SendMailResetPassword(obj.Email, member.Token);
                    if (result)
                        PushNotification(new NotificationOption
                        {
                            Type = "success",
                            Message = $"Email chứa liên kết thiết lập lại mật khẩu đã được gửi tới {obj.Email}."
                        });
                    else
                        PushNotification(new NotificationOption
                        {
                            Type = "error",
                            Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                        });
                    return Redirect("/auth/login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản này đã bị khóa.");
                    return View();
                }
            }
            ModelState.AddModelError(nameof(obj.Email), "Email không tồn tại");
            return View();
        }
        public IActionResult ResetPassword(string id)
        {
            Member member = provider.Member.GetMemberByToken(id);
            if (member != null)
            {
                return View();
            }
            PushNotification(new NotificationOption
            {
                Type = "error",
                Message = "Liên kết đã hết hạn hoặc không tồn tại."
            });            
            return Redirect("/auth/login");
        }
        [HttpPost]
        public IActionResult ResetPassword(string id, ResetPasswordModel obj)
        {
            if (!ModelState.IsValid)
                return View();
            obj.Token = id;
            provider.Member.ResetPassword(obj);
            PushNotification(new NotificationOption
            {
                Type = "success",
                Message = "Đã cập nhật mật khẩu mới."
            });

            return Redirect("/auth/login");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel obj)
        {
            if (!ModelState.IsValid)
                return View();
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Member member = provider.Member.GetMemberById(memberId);
            if (member != null)
            {
                member.Password = obj.OldPassword;
                bool paswordValid = provider.Member.CheckCurrentPassword(member);
                if (!paswordValid)
                {
                    ModelState.AddModelError(obj.OldPassword, "Mật khẩu cũ không đúng.");
                    return View();
                }
                member.Password = obj.NewPassword;
                provider.Member.UpdatePassword(member);
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Đã cập nhật mật khẩu mới."
                });
                return Redirect("/member");
            }
            return Redirect("/auth/login");

        }
    }
}
