using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IMemberRepository
    {
        int Add(Member obj);
        Member GetMemberByEmail(string email);
        IEnumerable<Member> Search(string query);
        Member Login(Member obj);
        Member GetMemberById(Guid memberId);
        IEnumerable<Member> GetMembers();
        int UpdateAccountStatus(Guid memberId);
        int SaveTokenOfMember(Member obj);
        Member GetMemberByToken(string token);
        int ResetPassword(ResetPasswordViewModel obj);
        bool CheckCurrentPassword(Member obj);
        int UpdatePassword(Member obj);
    }
}
