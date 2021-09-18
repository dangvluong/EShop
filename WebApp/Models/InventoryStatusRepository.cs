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
    public class InventoryStatusRepository : BaseRepository
    {
        //IConfiguration configuration;
        public InventoryStatusRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<InventoryStatus> GetInventoryStatusesByProduct(short productId)
        {
            return connection.Query<InventoryStatus>("GetInventoryStatusesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
        }

        public int GetInventoryStatusByProductColorAndSize(short productId, short colorId, byte sizeId)
        {
            string sql = $"SELECT Quantity FROM InventoryStatus WHERE ProductId = {productId} AND ColorId = {colorId} AND SizeId = {sizeId}";
            return connection.QuerySingleOrDefault<int>(sql);
        }
        public int UpdateInventoryQuantity(InvoiceDetail obj)
        {
            return connection.Execute("UpdateInventoryQuantityFromInvoice", new { ProductId = obj.ProductId, ColorId = obj.ColorId, SizeId = obj.SizeId, Quantity = obj.Quantity }, commandType: CommandType.StoredProcedure);
        }
        public int UpdateInventoryQuantity(List<InventoryStatus> list)
        {
            return connection.Execute("UpdateInventoryQuantity",list, commandType: CommandType.StoredProcedure);
        }
    }
}
