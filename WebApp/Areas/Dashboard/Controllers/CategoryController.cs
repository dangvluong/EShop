using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles ="Manager,Staff")]
    public class CategoryController : BaseController
    {
        public CategoryController(IRepositoryManager provider):base(provider)
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
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Chỉnh sửa danh mục thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });           
            return Json(result);
        }
        [HttpPost]
        public IActionResult Delete(short id)
        {
            int result = provider.Category.Delete(id);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Xóa danh mục thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });            
            return Json(result);
        }
        [HttpPost]
        public IActionResult Add(string categoryName)
        {
            int result = provider.Category.Add(categoryName);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Tạo danh mục thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });            
            return Redirect("/dashboard/category");
        }
    }
}
