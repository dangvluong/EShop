using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ColorRepository : BaseRepository,IColorRepository
    {
        //IConfiguration configuration;
        public ColorRepository(IDbConnection connection) : base(connection)
        {
            //this.configuration = configuration;
        }
        public List<Color> GetColorsByProduct(short productId)
        {
            return connection.Query<Color>("GetColorsByProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure).ToList();
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
        public IEnumerable<Statistic> GetBestSellingColors()
        {
            return connection.Query<Statistic>("GetBestSellingColors", commandType: CommandType.StoredProcedure);
        }
    }
}
