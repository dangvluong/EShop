using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class CategoryController : BaseController
    {
        public CategoryController(IConfiguration configuration):base(configuration)
        {

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
        [HttpPost]
        public IActionResult Add(string categoryName)
        {
            provider.Category.Add(categoryName);
            return Redirect("/dashboard/category");
        }
    }
}
