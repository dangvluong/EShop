using System.Threading.Tasks;

namespace WebApp.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMailResetPassword(string emailTo, string token);

    }
}
