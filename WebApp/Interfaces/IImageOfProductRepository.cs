using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IImageOfProductRepository
    {
        List<ImageOfProduct> GetImagesByProduct(short productId);
        int GetNumberImageExists(ImageOfProductUpload obj);
        int AddImageOfProduct(ImageOfProductUpload obj,string imageUrl);
        int Delete(ImageOfProduct obj);
    }
}
