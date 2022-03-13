using Dapper;
using System.Collections.Generic;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class DistrictRepository : BaseRepository
    {
        public DistrictRepository(IDbConnection connection) : base(connection) { }

        public IEnumerable<District> GetDistrictByProvince(short provinceId)
        {
            return connection.Query<District>("GetDistrictsByProvince", new { ProvinceId = provinceId }, commandType: CommandType.StoredProcedure);
        }

        public District GetDistrictById(short districtId)
        {
            return connection.QuerySingleOrDefault<District>("GetDistrictById", new { DistrictId = districtId }, commandType: CommandType.StoredProcedure);
        }
    }
}
