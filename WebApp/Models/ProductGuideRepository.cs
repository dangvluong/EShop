using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductGuideRepository : BaseRepository
    {
        public ProductGuideRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<ProductGuide> list)
        {
            var productId = list[0].ProductId;
            connection.Execute($"DELETE FROM GuideOfProduct WHERE ProductId = {productId}");
            return connection.Execute("AddGuideOfProduct", list, commandType: CommandType.StoredProcedure);
        }
    }
}
