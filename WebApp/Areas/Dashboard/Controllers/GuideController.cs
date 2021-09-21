using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager, Staff")]
    public class GuideController : BaseController
    {        
        int size = 50;
        public GuideController(SiteProvider provider) :base(provider)
        {
            
        }
        public IActionResult Index()
        {
            return View(provider.Guide.GetGuids());
        }
        public IActionResult Edit(Guide obj)
        {
            int result = provider.Guide.Edit(obj);
            string[] message = { "Lỗi", "Cập nhật thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Guide.Delete(id);
            string[] message = { "Lỗi", "Xóa thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
        public IActionResult Add(Guide obj)
        {
            int result = provider.Guide.Add(obj);
            string[] message = { "Lỗi", "Thêm thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
    }
}
