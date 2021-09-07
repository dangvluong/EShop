using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
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
