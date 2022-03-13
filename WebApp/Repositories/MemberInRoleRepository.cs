using Dapper;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories
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
