using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager,Staff")]
    public class GuideController : BaseController
    {
        public GuideController(IRepositoryManager provider) : base(provider)
        {

        }
        public IActionResult Index()
        {
            return View(provider.Guide.GetGuides());
        }
        public IActionResult Edit(Guide obj)
        {
            int result = provider.Guide.Edit(obj);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Chỉnh sửa hướng dẫn sử dụng thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });           
            return Redirect("/dashboard/guide");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Guide.Delete(id);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Xóa hướng dẫn sử dụng thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });          
            return Redirect("/dashboard/guide");
        }
        public IActionResult Add(Guide obj)
        {
            int result = provider.Guide.Add(obj);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Thêm hướng dẫn sử dụng thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });           
            return Redirect("/dashboard/guide");
        }
    }
}
