using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ISizeRepository
    {
        IEnumerable<Size> GetSizes();
        List<Size> GetSizesByProduct(short productId);
        int Edit(Size obj);
        int Delete(short id);
        int Add(Size obj);
        IEnumerable<Statistic> GetBestSellingSizes();
    }
}
