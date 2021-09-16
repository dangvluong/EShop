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
    public class ColorRepository : BaseRepository
    {
        //IConfiguration configuration;
        public ColorRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }
        public List<Color> GetColorsByProduct(short productId)
        {
            return connection.Query<Color>("GetColorByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public IEnumerable<Color> GetColors()
        {
            return connection.Query<Color>("SELECT * FROM Color");
        }
    }
}
