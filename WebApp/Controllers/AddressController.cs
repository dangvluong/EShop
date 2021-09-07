using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{   
    public class AddressController : Controller
    {
        SiteProvider provider;
        public AddressController(IConfiguration configuration)
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

    }
}
