using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetRandom12Products();
        IEnumerable<Product> GetProducts(int page, int size, out int total);
        IEnumerable<Product> GetAllProduct();
        Product GetProductById(short id);
        IEnumerable<Product> GetProductsByCategory(short categoryId, int page, int size, out int total);
        ICollection<Product> SearchProduct(string query, int page, int size, out int total);
        int Edit(Product obj);
        short Add(Product obj);
        int Delete(short id);
        IEnumerable<Statistic> GetBestSellingProduct();
        IEnumerable<Statistic> GetTop5HighestInventoryProducts();
        List<Product> GetProductsRelation(short productId);
    }
}
