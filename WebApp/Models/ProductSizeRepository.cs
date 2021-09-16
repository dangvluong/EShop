using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductSizeRepository :BaseRepository
    {
        public ProductSizeRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<ProductSize> list)
        {
            var productId = list[0].ProductId;
            connection.Execute($"DELETE FROM SizeOfProduct WHERE ProductId = {productId}");
            return connection.Execute("AddSizeOfProduct", list, commandType: CommandType.StoredProcedure);
        }
        public int Add(List<ProductSize> list)
        {            
            return connection.Execute("AddSizeOfProduct", list, commandType: CommandType.StoredProcedure);
        }
    }
}
