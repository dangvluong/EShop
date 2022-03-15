using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager,Staff")]
    public class SizeController : BaseController
    {

        public SizeController(IRepositoryManager provider) : base(provider)
        {
        }
        public IActionResult Index()
        {
            return View(provider.Size.GetSizes());
        }
        public IActionResult Edit(Size obj)
        {
            int result = provider.Size.Edit(obj);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Cập nhật kích thước thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
            return Redirect("/dashboard/size");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Size.Delete(id);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Xóa kích thước thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
            return Redirect("/dashboard/size");
        }
        [HttpPost]
        public IActionResult Add(Size obj)
        {
            int result = provider.Size.Add(obj); 
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Tạo mới kích thước thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });            
            return Redirect("/dashboard/size");
        }
    }
}
