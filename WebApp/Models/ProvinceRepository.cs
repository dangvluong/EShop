using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProvinceRepository : BaseRepository
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
