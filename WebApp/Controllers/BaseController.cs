using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected SiteProvider provider;
        public BaseController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
    }
}
