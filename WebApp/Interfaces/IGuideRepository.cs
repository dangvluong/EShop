using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IGuideRepository
    {
        IEnumerable<Guide> GetGuides();
        IEnumerable<Guide> GetGuidesByProduct(short productId);
        int Edit(Guide obj);
        int Delete(short id);
        int Add(Guide obj);
    }
}
