using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ISizeOfProductRepository
    {
        int Edit(List<SizeOfProduct> list, short productId);
        int Add(List<SizeOfProduct> list);

    }
}
