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
            return connection.Execute("AddMember", new {
                Username = obj.Username,
                Password = Helper.HashPassword(obj.Password),
                Email = obj.Email,
                Gender = obj.Gender,
                JoinDate = DateTime.UtcNow
            }, commandType: CommandType.StoredProcedure);
        }

        public  Member Login(Member obj)
        {
            return connection.QuerySingleOrDefault<Member>("Login",
                new { Username = obj.Username,
                    Password = Helper.HashPassword(obj.Password) },
                commandType: CommandType.StoredProcedure); 
        }
    }
}
