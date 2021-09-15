using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WebApp.Models
{
    public class SizeRepository : BaseRepository
    {
        //IConfiguration configuration;
        public SizeRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<Size> GetSizes()
        {
            return connection.Query<Size>("GetSizes", commandType: CommandType.StoredProcedure);

        }

        public List<Size> GetSizesByProduct(short productId)
        {
            return connection.Query<Size>("GetSizesByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
