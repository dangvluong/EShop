using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductCategoryRepository : BaseRepository
    {
        public ProductCategoryRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<ProductCategory> list)
        {
            var productId = list[0].ProductId;
            connection.Execute($"DELETE FROM ProductInCategory WHERE ProductId = {productId}");
            return connection.Execute("AddProductInCategory", list, commandType: CommandType.StoredProcedure);
        }
        public int Add(List<ProductCategory> list)
        {            
            return connection.Execute("AddProductInCategory", list, commandType: CommandType.StoredProcedure);
        }
    }
}
