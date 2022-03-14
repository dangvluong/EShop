using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoriesByProduct(short productId);
        int Edit(Category obj);
        int Delete(short id);
        int Add(string categoryName);
    }
}
