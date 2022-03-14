using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class GuideOfProductRepository : BaseRepository,IGuideOfProductRepository
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
