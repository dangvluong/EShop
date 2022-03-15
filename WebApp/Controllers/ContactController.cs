using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        public ContactController(IRepositoryManager provider) : base(provider)
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
            return Json(provider.District.GetDistrictsByProvince(provinceId));
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
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Đã thêm thông tin liên hệ."
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
        public IActionResult GetContactsByMember(Guid memberId)
        {
            Console.WriteLine(memberId);
            return Json(provider.Contact.GetContactsByMember(memberId));
        }
        [HttpPost]
        public IActionResult UpdateContact(Contact obj)
        {
            int result = provider.Contact.Update(obj);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Đã cập nhật thông tin liên hệ."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
            return Redirect("/member");
        }

        public IActionResult DeleteContact(short id)
        {
            int result = provider.Contact.Delete(id);
            if (result > 0)
                PushNotification(new NotificationOption
                {
                    Type = "success",
                    Message = "Đã xóa thông tin liên hệ."
                });
            else
                PushNotification(new NotificationOption
                {
                    Type = "error",
                    Message = "Có lỗi xảy ra. Vui lòng thử lại sau."
                });
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
            return Json(provider.Contact.GetContactById(id));
        }
    }
}
