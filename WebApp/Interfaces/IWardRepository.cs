using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IWardRepository
    {
        IEnumerable<Ward> GetWardsByDistrict(short districtId);
        Ward GetWardById(short warId);

    }
}
