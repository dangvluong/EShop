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
    public class CategoryRepository
    {
        IConfiguration configuration;
        public CategoryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<Category> GetCategories()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Category>("GetCategories", commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<Category> GetCategoriesByProduct(short productId)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Category>("GetCategoriesByProduct",new {ProductId = productId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
