using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IGuideOfProductRepository
    {
        int Edit(List<GuideOfProduct> list, short productId);
        int Add(List<GuideOfProduct> list);
    }
}
