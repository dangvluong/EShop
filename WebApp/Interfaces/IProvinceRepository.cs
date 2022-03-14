using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IProvinceRepository
    {
        IEnumerable<Province> GetProvinces();
        Province GetProvinceById(short provinceId);

    }
}
