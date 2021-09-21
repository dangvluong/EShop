using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class GuideOfProductRepository : BaseRepository
    {
        public GuideOfProductRepository(IDbConnection connection) : base(connection) { }
        public int Edit(List<GuideOfProduct> list, short productId)
        {
            connection.Execute($"DELETE FROM GuideOfProduct WHERE ProductId = {productId}");
            return connection.Execute("AddGuideOfProduct", list, commandType: CommandType.StoredProcedure);
        }
        public int Add(List<GuideOfProduct> list)
        {
            return connection.Execute("AddGuideOfProduct", list, commandType: CommandType.StoredProcedure);
        }
    }
}
