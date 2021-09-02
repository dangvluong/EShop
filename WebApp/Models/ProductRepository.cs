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
    public class ProductRepository
    {
        IConfiguration configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<Product> GetProducts()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Product>("GetProducts", commandType: CommandType.StoredProcedure);
            }
        }
        public Product GetProductById(short id)
        {
            using(IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.QueryFirstOrDefault<Product>("GetProductById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
