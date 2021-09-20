using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class GuideController : BaseController
    {        
        int size = 50;
        public GuideController(IConfiguration configuration):base(configuration)
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
