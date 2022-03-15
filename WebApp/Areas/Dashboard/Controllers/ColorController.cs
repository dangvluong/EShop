using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager,Staff")]
    public class ColorController : BaseController
    {
        string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "icon");
        public ColorController(IRepositoryManager provider) : base(provider)
        {

        }
        public IActionResult Index()
        {
            IEnumerable<Color> colors = provider.Color.GetColors();
            return View(colors);
        }
        public IActionResult Edit(IFormFile iconUpload, Color obj)
        {
            if (iconUpload != null && !string.IsNullOrEmpty(iconUpload.FileName))
            {
                obj.IconUrl = obj.ColorCode + Path.GetExtension(iconUpload.FileName);
                string path = Path.Combine(root, obj.IconUrl);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    iconUpload.CopyTo(stream);
                }
            }
            int result = provider.Color.Edit(obj);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Chỉnh sửa màu sắc thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
            return Redirect("/dashboard/color");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Color.Delete(id);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Xóa màu sắc thành công."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
            return Redirect("/dashboard/color");
        }
        public IActionResult Add(IFormFile iconUpload, Color obj)
        {
            bool colorExist = provider.Color.CheckColorExist(obj);            
            if (colorExist)
            {
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Đã tồn tại màu sắc này."
                });
            }
            else
            {
                if (iconUpload != null && !string.IsNullOrEmpty(iconUpload.FileName))
                {
                    obj.IconUrl = obj.ColorCode + Path.GetExtension(iconUpload.FileName);
                    string path = Path.Combine(root, obj.IconUrl);
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        iconUpload.CopyTo(stream);
                    }
                }
                int result = provider.Color.AddColor(obj);
                if (result > 0)
                    PushNotification(new NotificationOption
                    {
                        Type = "success",
                        Message = "Thêm màu sắc thành công."
                    });
                else
                    PushNotification(new NotificationOption
                    {
                        Type = "error",
                        Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                    });
            }            
            return Redirect("/dashboard/color");
        }
    }
}
