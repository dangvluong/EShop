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
    public class ColorRepository
    {
        IConfiguration configuration;
        public ColorRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<Color> GetColorsByProduct(short productId)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Color>("GetColorByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
