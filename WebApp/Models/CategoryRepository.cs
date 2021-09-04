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
    public class CategoryRepository : BaseRepository
    {
        //IConfiguration configuration;
        public CategoryRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;   
        }
        public IEnumerable<Category> GetCategories()
        {
            return connection.Query<Category>("GetCategories", commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Category> GetCategoriesByProduct(short productId)
        {
            return connection.Query<Category>("GetCategoriesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
        }
    }
}
