using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IRepositoryManager provider;
        protected IConfiguration configuration;
        public BaseController(IRepositoryManager provider)
        {
            //provider = new SiteProvider(configuration);
            this.provider = provider;
        }
        public BaseController(IRepositoryManager provider, IConfiguration configuration)
        {
            this.provider = provider;
            this.configuration = configuration;
        }
        protected void PushNotification(NotificationOption option)
        {
            TempData["notification"] = JsonConvert.SerializeObject(option);
        }
    }
}
