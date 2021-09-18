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
            return connection.Query<Color>("SELECT * FROM Color WHERE IsDeleted = 0");
        }
        public int Edit(Color obj)
        {
            return connection.Execute($"UPDATE Color SET ColorCode = '{obj.ColorCode}', IconUrl = '{obj.IconUrl}' WHERE ColorId = {obj.ColorId}");
        }
        public int Delete(short id)
        {
            return connection.Execute("DeleteColor", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
        public bool CheckColorExist(Color obj)
        {
            int result =  connection.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM Color WHERE ColorCode = '{obj.ColorCode.Trim()}'");
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public int AddColor(Color obj)
        {
            return connection.Execute("AddColor", new { ColorCode = obj.ColorCode, IconUrl = obj.IconUrl }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Statistic> GetBestSellingColor()
        {
            return connection.Query<Statistic>("GetBestSellingColor", commandType: CommandType.StoredProcedure);
        }
    }
}
