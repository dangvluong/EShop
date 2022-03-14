using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class RoleRepository : BaseRepository,IRoleRepository
    {
        public RoleRepository(IDbConnection connection) : base(connection) { }
        public IEnumerable<Role> GetRoles()
        {
            return connection.Query<Role>("SELECT * FROM Role");
        }
        public IEnumerable<Role> GetRolesByMember(Guid memberId)
        {
            return connection.Query<Role>("GetRolesByMember", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }
    }
}
