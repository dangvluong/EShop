using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RoleRepository : BaseRepository
    {
        public RoleRepository(IDbConnection connection) : base(connection) { }
        public IEnumerable<Role> GetRoles()
        {
            return connection.Query<Role>("SELECT * FROM Role");
        }
        public IEnumerable<string> GetRolesNameByMember(Guid memberId)
        {
            return connection.Query<string>("GetRolesNameByMember", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }

    }
}
