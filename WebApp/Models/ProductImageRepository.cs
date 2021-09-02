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
    public class ProductImageRepository
    {
        IConfiguration configuration;
        public ProductImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<ProductImage> GetImagesByProduct(short productId)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<ProductImage>("GetImagesByProduct",new { ProductId = productId}, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
