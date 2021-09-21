using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles ="Manager, Staff")]
    public class CategoryController : BaseController
    {
        public CategoryController(SiteProvider provider):base(provider)
        {

        }
        public IActionResult Index()
        {
            return View(provider.Category.GetCategories());
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            int result = provider.Category.Edit(obj);
            string[] msg = { "Có lỗi xảy ra", "Chỉnh sửa danh mục thành công" };
            TempData["msg"] = msg[result];
            return Json(result);
        }
        [HttpPost]
        public IActionResult Delete(short id)
        {
            int result = provider.Category.Delete(id);
            string[] msg = { "Có lỗi xảy ra", "Xóa danh mục thành công" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Json(result);
        }
        [HttpPost]
        public IActionResult Add(string categoryName)
        {
            int result = provider.Category.Add(categoryName);
            string[] msg = { "Có lỗi xảy ra", "Tạo danh mục thành công" };
            TempData["msg"] = msg[result];
            return Redirect("/dashboard/category");
        }
    }
}
