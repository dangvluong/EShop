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
        public ColorController(IRepositoryManager provider) :base(provider)
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
            string[] msg = {"Có lỗi xảy ra", "Chỉnh sửa màu sắc thành công" };
            TempData["msg"] = msg[result];
            return Redirect("/dashboard/color");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Color.Delete(id);
            string[] msg = { "Có lỗi xảy ra", "Xóa màu sắc thành công" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Redirect("/dashboard/color");
        }
        public IActionResult Add(IFormFile iconUpload, Color obj)
        {            
            bool colorExist = provider.Color.CheckColorExist(obj);
            int indexMessage = 0;
            if (colorExist)
            {
                indexMessage = -1;
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
                indexMessage = provider.Color.AddColor(obj);                
            }
            string[] message = { "Đã tồn tại màu sắc này", "Có lỗi xảy ra", "Thêm màu sắc thành công" };
            TempData["msg"] = message[indexMessage + 1];
            return Redirect("/dashboard/color");
        }
    }
}
