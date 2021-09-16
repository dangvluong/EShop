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
    public class ProductImageRepository : BaseRepository
    {
        //IConfiguration configuration;
        public ProductImageRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }
        public List<ProductImage> GetImagesByProduct(short productId)
        {
            return connection.Query<ProductImage>("GetImagesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public int GetNumberImageExists(ProductImageUpload obj)
        {
            return connection.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM ProductImage WHERE ProductId = {obj.ProductId} AND ColorId = {obj.ColorId}");
        }
        public int AddProductImage(ProductImageUpload obj, string imageUrl)
        {
            return connection.Execute("AddProductImage", new { ProductId = obj.ProductId, ColorId = obj.ColorId, ImageUrl = imageUrl }, commandType: CommandType.StoredProcedure);
        }        
    }
}
