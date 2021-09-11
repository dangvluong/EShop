using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MemberRepository:BaseRepository
    {
        public MemberRepository(IDbConnection connection) : base(connection) { }
        public int Add(Member obj)
        {
            int result = 0;
            try
            {
                connection.Execute("AddMember", new
                {
                    Username = obj.Username,
                    Password = Helper.HashPassword(obj.Password),
                    Email = obj.Email,
                    Gender = obj.Gender,
                    JoinDate = DateTime.UtcNow
                }, commandType: CommandType.StoredProcedure);
                result = 1;
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        public  Member Login(Member obj)
        {
            return connection.QuerySingleOrDefault<Member>("Login",
                new { Username = obj.Username,
                    Password = Helper.HashPassword(obj.Password) },
                commandType: CommandType.StoredProcedure); 
        }
        public Member GetMemberById(Guid memberId)
        {
            return connection.QuerySingleOrDefault<Member>("GetMemberById", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Member> GetMembers()
        {
            return connection.Query<Member>("SELECT MemberId, Username, Email, Gender, JoinDate, IsBanned FROM Member");
        }
        public int UpdateAccountStatus(Guid memberId)
        {
            return connection.Execute("UpdateAccountStatus", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }
    }
}
