using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IProductInCategoryRepository
    {
        int Edit(List<ProductInCategory> list, short productId);
        int Add(List<ProductInCategory> list);
    }
}
