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
    public class InventoryQuantityRepository : BaseRepository
    {
        //IConfiguration configuration;
        public InventoryQuantityRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<InventoryQuantity> GetInventoryQuantitiesByProduct(short productId)
        {
            return connection.Query<InventoryQuantity>("GetInventoryQuantitiesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
        }

        public int GetInventoryQuantitiesByProductColorAndSize(short productId, short colorId, short sizeId)
        {
            string sql = $"SELECT Quantity FROM InventoryQuantity WHERE ProductId = {productId} AND ColorId = {colorId} AND SizeId = {sizeId}";
            return connection.QuerySingleOrDefault<int>(sql);
        }
        public int UpdateInventoryQuantity(InvoiceDetail obj)
        {
            return connection.Execute("UpdateInventoryQuantityFromInvoice", new { ProductId = obj.ProductId, ColorId = obj.ColorId, SizeId = obj.SizeId, Quantity = obj.Quantity }, commandType: CommandType.StoredProcedure);
        }
        public int UpdateInventoryQuantity(List<InventoryQuantity> list)
        {
            return connection.Execute("UpdateInventoryQuantity",list, commandType: CommandType.StoredProcedure);
        }
    }
}
