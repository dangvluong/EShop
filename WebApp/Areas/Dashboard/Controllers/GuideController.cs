﻿using Microsoft.AspNetCore.Authorization;
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
            string[] message = { "Có lỗi xảy ra", "Chỉnh sửa hướng dẫn sử dụng thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Guide.Delete(id);
            string[] message = { "Có lỗi xảy ra", "Xóa hướng dẫn sử dụng thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
        public IActionResult Add(Guide obj)
        {
            int result = provider.Guide.Add(obj);
            string[] message = { "Có lỗi xảy ra", "Thêm hướng dẫn sử dụng thành công" };
            TempData["msg"] = message[result];
            return Redirect("/dashboard/guide");
        }
    }
}
