using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{   
    [Authorize]
    public class ContactController : Controller
    {
        SiteProvider provider;
        public ContactController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
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
            return Json(provider.Contact.Add(obj, memberId));
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
            return Json(provider.Contact.Update(obj));
        }

        public IActionResult DeleteContact(short id)
        {
            provider.Contact.Delete(id);
            return Redirect("/member");
        }
        [HttpPost]
        public IActionResult UpdateDefaultContact(short contactId)
        {
            Console.WriteLine("Update COntact");
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(memberId);
            return Json(provider.Contact.UpdateDefaultContact(memberId, contactId));
        }       
    }
}
