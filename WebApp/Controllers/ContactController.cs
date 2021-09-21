using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        public ContactController(SiteProvider provider) : base(provider)
        {          
        }
        [HttpPost]
        public IActionResult GetProvinces()
        {            
            return Json(provider.Province.GetProvinces());
        }

        [HttpPost]
        public IActionResult GetDistrictsByProvince(short provinceId)
        {
            return Json(provider.District.GetDistrictByProvince(provinceId));
        }
        [HttpPost]
        public IActionResult GetWardsByDistrict(short districtId)
        {
            return Json(provider.Ward.GetWardsByDistrict(districtId));
        }
        [HttpPost]
        public IActionResult Add(Contact obj)
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int result = provider.Contact.Add(obj, memberId);
            string[] msg = { "Có lỗi xảy ra", "Đã cập nhật thông tin liên hệ" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetContactsByMember(Guid memberId)
        {
            Console.WriteLine(memberId);
            return Json(provider.Contact.GetContactsByMember(memberId));
        }
        [HttpPost]
        public IActionResult UpdateContact(Contact obj)
        {
            int result = provider.Contact.Update(obj);
            string[] msg = { "Có lỗi xảy ra", "Đã cập nhật thông tin liên hệ" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Redirect("/member");
        }

        public IActionResult DeleteContact(short id)
        {           
            int result = provider.Contact.Delete(id);
            string[] msg = { "Có lỗi xảy ra", "Đã xóa thông tin liên hệ" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Redirect("/member");
        }
        [HttpPost]
        public IActionResult UpdateDefaultContact(short contactId)
        {            
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(memberId);
            return Json(provider.Contact.UpdateDefaultContact(memberId, contactId));
        }
        [HttpPost]
        public IActionResult GetContactById(short id)
        {
            return Json(provider.Contact.GetContactsById(id));
        }
    }
}
