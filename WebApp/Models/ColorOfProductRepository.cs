using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ColorOfProductRepository : BaseRepository
    {
        public ColorOfProductRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<ColorOfProduct> list)
        {
            var productId = list[0].ProductId;
            connection.Execute($"DELETE FROM ColorOfProduct WHERE ProductId = {productId}");
            return connection.Execute("AddColorOfProduct", list, commandType: CommandType.StoredProcedure);
        }

        public int Add(List<ColorOfProduct> list)
        {            
            return connection.Execute("AddColorOfProduct", list, commandType: CommandType.StoredProcedure);
        }
    }
}
