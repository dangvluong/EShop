using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models
{
    public class SizeRepository:BaseRepository
    {
        //IConfiguration configuration;
        public SizeRepository(IConfiguration configuration):base(configuration)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<Size> GetSizes()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Size>("GetSizes", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Size> GetSizesByProduct(short productId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Size>("GetSizesByProduct",new {ProductId = productId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
