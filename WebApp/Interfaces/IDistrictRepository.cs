using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IDistrictRepository
    {
        IEnumerable<District> GetDistrictsByProvince(short provinceId);
        District GetDistrictById(short districtId);
    }
}
