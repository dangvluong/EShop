using Microsoft.AspNetCore.Mvc;
using WebApp.Helper;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected SiteProvider provider;
        public BaseController(SiteProvider provider)
        {
            //provider = new SiteProvider(configuration);
            this.provider = provider;
        }
    }
}
