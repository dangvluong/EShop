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
    public class SizeRepository
    {
        IConfiguration configuration;
        public SizeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Size> GetSizes()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Size>("GetSizes", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Size> GetSizesByProduct(short productId)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("EzShop")))
            {
                return connection.Query<Size>("GetSizesByProduct",new {ProductId = productId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
