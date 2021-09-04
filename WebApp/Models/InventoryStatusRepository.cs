using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class InventoryStatusRepository
    {
        IConfiguration configuration;
        public InventoryStatusRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<InventoryStatus> GetInventoryStatusesByProduct(short productId)
        {
            using(IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<InventoryStatus>("GetInventoryStatusesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
            }
        }

        public int GetInventoryStatusByProductColorAndSize(short productId, short colorId, byte sizeId)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                string sql = $"SELECT Quantity FROM InventoryStatus WHERE ProductId = {productId} AND ColorId = {colorId} AND SizeId = {sizeId}";
                return connection.QuerySingleOrDefault<int>(sql);
            }
        }
    }
}
