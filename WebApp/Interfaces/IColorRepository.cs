using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IColorRepository
    {
        List<Color> GetColorsByProduct(short productId);
        IEnumerable<Color> GetColors();
        int Edit(Color obj);
        int Delete(short id);
        bool CheckColorExist(Color obj);
        int AddColor(Color obj);
        IEnumerable<Statistic> GetBestSellingColors();
    }
}
