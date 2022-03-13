using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        //IConfiguration configuration;
        public CategoryRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;   
        }
        public IEnumerable<Category> GetCategories()
        {
            return connection.Query<Category>("GetCategories", commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Category> GetCategoriesByProduct(short productId)
        {
            return connection.Query<Category>("GetCategoriesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
        }
        public int Edit(Category obj)
        {
            return connection.Execute($"UPDATE Category SET CategoryName = N'{obj.CategoryName}' WHERE CategoryId = {obj.CategoryId}");
        }
        public int Delete(short id)
        {
            return connection.Execute("DeleteCategory", new { CategoryId = id }, commandType: CommandType.StoredProcedure);
        }
        public int Add(string categoryName)
        {
            return connection.Execute($"INSERT INTO Category(CategoryName) VALUES(N'{categoryName}')");
        }
    }
}
