using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class SizeController : BaseController
    {        
        
        public SizeController(IConfiguration configuration):base(configuration)
        {            
        }
        public IActionResult Index()
        {
            return View(provider.Size.GetSizes());
        }
        public IActionResult Edit(Size obj)
        {
            int result = provider.Size.Edit(obj);
            string[] message = {"Lỗi", "Cập nhật thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/size");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Size.Delete(id);
            string[] message = {"Lỗi", "Xóa thành công" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = message[result ];
            return Redirect("/dashboard/size");
        }
        [HttpPost]
        public IActionResult Add(Size obj)
        {
            int result = provider.Size.Add(obj);
            string[] message = {"Lỗi", "Tạo mới thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/size");
        }
    }
}
