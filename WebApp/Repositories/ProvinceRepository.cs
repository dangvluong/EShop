using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ProvinceRepository : BaseRepository,IProvinceRepository
    {
        public ProvinceRepository(IDbConnection connection) : base(connection) {        
        }

        public IEnumerable<Province> GetProvinces()
        {
            return connection.Query<Province>("GetProvinces", commandType: CommandType.StoredProcedure);
        }
        public Province GetProvinceById(short provinceId)
        {
            return connection.QuerySingleOrDefault<Province>("GetProvinceById",new { ProvinceId = provinceId}, commandType: CommandType.StoredProcedure);
        }
    }
}
