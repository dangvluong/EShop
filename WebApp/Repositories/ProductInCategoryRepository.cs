using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ProductInCategoryRepository : BaseRepository,IProductInCategoryRepository
    {
        public ProductInCategoryRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<ProductInCategory> list, short productId)
        {
            connection.Execute($"DELETE FROM ProductInCategory WHERE ProductId = {productId}");
            return connection.Execute("AddProductInCategory", list, commandType: CommandType.StoredProcedure);
        }
        public int Add(List<ProductInCategory> list)
        {            
            return connection.Execute("AddProductInCategory", list, commandType: CommandType.StoredProcedure);
        }
    }
}
