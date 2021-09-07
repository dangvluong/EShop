using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class WardRepository : BaseRepository
    {
        public WardRepository(IDbConnection connection) : base(connection) { }
        public IEnumerable<Ward> GetWardsByDistrict(short districtId)
        {
            return connection.Query<Ward>("GetWardsByDistrict", new { DistrictId = districtId }, commandType: CommandType.StoredProcedure);
        }
        public Ward GetWardById(short warId)
        {
            return connection.QuerySingleOrDefault<Ward>("GetWardById", new { WardId = warId }, commandType: CommandType.StoredProcedure);
        }
    }
}
