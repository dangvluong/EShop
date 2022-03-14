using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class WardRepository : BaseRepository,IWardRepository
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
