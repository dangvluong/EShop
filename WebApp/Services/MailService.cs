using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class MailService : IMailService
    {
        IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> SendMailResetPassword(string emailTo, string token)
        {
            IConfigurationSection mailSection = configuration.GetSection("Email:Outlook");
            using (SmtpClient client = new SmtpClient(mailSection["host"], Convert.ToInt32(mailSection["port"]))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailSection["address"], mailSection["password"]),
                EnableSsl = true
            })
            {
                MailAddress addressFrom = new MailAddress(mailSection["address"]);
                MailAddress addressTo = new MailAddress(emailTo.Trim());
                using (MailMessage message = new MailMessage(addressFrom, addressTo))
                {
                    message.IsBodyHtml = true;
                    message.Body = $"Vui lòng click vào <a href=\"https://localhost:44389/auth/resetpassword/{token}\">ĐÂY</a> để thiết lập lại mật khẩu của bạn. ";
                    message.Subject = "CẬP NHẬT MẬT KHẨU";
                    try
                    {
                        await client.SendMailAsync(message);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
