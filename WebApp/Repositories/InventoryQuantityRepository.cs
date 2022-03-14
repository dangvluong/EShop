using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class InventoryQuantityRepository : BaseRepository,IInventoryQuantityRepository
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

        public int GetInventoryQuantityByProductColorAndSize(short productId, short colorId, short sizeId)
        {
            string sql = $"SELECT Quantity FROM InventoryQuantity WHERE ProductId = {productId} AND ColorId = {colorId} AND SizeId = {sizeId}";
            return connection.QuerySingleOrDefault<int>(sql);
        }
        public int UpdateInventoryQuantityFromInvoice(InvoiceDetail obj)
        {
            return connection.Execute("UpdateInventoryQuantityFromInvoice", new { ProductId = obj.ProductId, ColorId = obj.ColorId, SizeId = obj.SizeId, Quantity = obj.Quantity }, commandType: CommandType.StoredProcedure);
        }
        public int UpdateInventoryQuantity(List<InventoryQuantity> list)
        {
            return connection.Execute("UpdateInventoryQuantity",list, commandType: CommandType.StoredProcedure);
        }
    }
}
