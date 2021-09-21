using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager, Staff")]
    public class SizeController : BaseController
    {        
        
        public SizeController(SiteProvider provider) : base(provider)
        {            
        }
        public IActionResult Index()
        {
            return View(provider.Size.GetSizes());
        }
        public IActionResult Edit(Size obj)
        {
            int result = provider.Size.Edit(obj);
            string[] message = {"Có lỗi xảy ra", "Cập nhật kích thước thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/size");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Size.Delete(id);
            string[] message = { "Có lỗi xảy ra", "Xóa kích thước thành công" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = message[result ];
            return Redirect("/dashboard/size");
        }
        [HttpPost]
        public IActionResult Add(Size obj)
        {
            int result = provider.Size.Add(obj);
            string[] message = { "Có lỗi xảy ra", "Tạo mới kích thước thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/size");
        }
    }
}
