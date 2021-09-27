using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Helper;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected SiteProvider provider;
        protected IConfiguration configuration;
        public BaseController(SiteProvider provider)
        {
            //provider = new SiteProvider(configuration);
            this.provider = provider;
        }
        public BaseController(SiteProvider provider, IConfiguration configuration)
        {
            this.provider = provider;
            this.configuration = configuration;
        }
    }
}
