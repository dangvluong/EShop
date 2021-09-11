using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MemberInRoleRepository :BaseRepository
    {
        public MemberInRoleRepository(IDbConnection connection) : base(connection) { }
        public int Add(MemberInRole obj)
        {
            return connection.Execute("AddMemberInRole", obj, commandType: CommandType.StoredProcedure);
        }
    }
}
