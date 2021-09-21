using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductInCategoryRepository : BaseRepository
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
