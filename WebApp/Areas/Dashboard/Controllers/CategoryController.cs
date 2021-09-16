using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class CategoryController : Controller
    {
        SiteProvider provider;
        public CategoryController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            return View(provider.Category.GetCategories());
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            return Json(provider.Category.Edit(obj));
        }
        [HttpPost]
        public IActionResult Delete(short id)
        {
            return Json(provider.Category.Delete(id));
        }
    }
}
