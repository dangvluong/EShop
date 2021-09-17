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
        public int Edit(Size obj)
        {
            return connection.Execute($"UPDATE Size SET SizeCode = '{obj.SizeCode}' WHERE SizeId = {obj.SizeId}");
        }
        public int Delete(short id)
        {
            return connection.Execute($"UPDATE Size SET IsDeleted = 1 WHERE SizeId = {id}");
        }
        public int Add(Size obj)
        {
            return connection.Execute($"INSERT INTO Size(SizeCode) VALUES('{obj.SizeCode}')");
        }
        public IEnumerable<Statistic> GetBestSellingSize()
        {
            return connection.Query<Statistic>("GetBestSellingSize", commandType: CommandType.StoredProcedure);
        }
    }
}
