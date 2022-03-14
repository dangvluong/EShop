using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using WebApp.Helper;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class MemberRepository : BaseRepository,IMemberRepository
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
                    Password = SiteHelper.HashPassword(obj.Password),
                    Email = obj.Email,
                    Gender = obj.Gender,
                    JoinDate = DateTime.UtcNow
                }, commandType: CommandType.StoredProcedure);
                result = 1;
            }
            catch
            {
                //result = -1;
            }
            return result;
        }
        public Member GetMemberByEmail(string email)
        {
            return connection.QuerySingleOrDefault<Member>($"SELECT MemberId, Username, Email, Gender, JoinDate, IsBanned FROM Member WHERE Email = '{email}'");
        }
        public IEnumerable<Member> Search(string query)
        {
            return connection.Query<Member>("SearchMember", new { Query = "%" + query + "%" }, commandType: CommandType.StoredProcedure);
        }
        public Member Login(Member obj)
        {
            return connection.QuerySingleOrDefault<Member>("Login",
                new
                {
                    Username = obj.Username,
                    Password = SiteHelper.HashPassword(obj.Password)
                },
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
        public int SaveTokenOfMember(Member obj)
        {
            return connection.Execute($"UPDATE Member SET Token = '{obj.Token}' WHERE MemberId = '{obj.MemberId}'");
        }
        public Member GetMemberByToken(string token)
        {
            return connection.QuerySingleOrDefault<Member>($"SELECT MemberId FROM Member WHERE Token ='{token}'");
        }
        public int ResetPassword(ResetPasswordModel obj)
        {
            return connection.Execute("ResetPassword", new
            {
                Token = obj.Token,
                NewPassword = SiteHelper.HashPassword(obj.NewPassword)
            }, commandType: CommandType.StoredProcedure);
        }
        public bool CheckCurrentPassword(Member obj)
        {
            Member result = connection.QuerySingleOrDefault<Member>("Login",
               new
               {
                   Username = obj.Username,
                   Password = SiteHelper.HashPassword(obj.Password)
               },
               commandType: CommandType.StoredProcedure);
            return result != null ? true : false;
        }
        public int UpdatePassword(Member obj)
        {
            return connection.Execute("UpdatePassword", new { MemberId = obj.MemberId, Password = SiteHelper.HashPassword(obj.Password) }, commandType: CommandType.StoredProcedure);
        }

        public Member GetMemberByUsername(string username)
        {
            return connection.QuerySingleOrDefault<Member>($"SELECT MemberId, Username, Email, Gender, JoinDate, IsBanned FROM Member WHERE Username = '{username}'");
        }
    }
}
