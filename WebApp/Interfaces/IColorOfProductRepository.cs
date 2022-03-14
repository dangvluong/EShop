using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IColorOfProductRepository
    {
        int Edit(List<ColorOfProduct> list, short productId);
        int Add(List<ColorOfProduct> list);
    }
}
