using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Controllers;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager")]
    public class MemberController : BaseController
    {        
        public MemberController(IRepositoryManager provider) : base(provider)
        {
            
        }        
        public IActionResult Index()
        {
            IEnumerable<Member> members = provider.Member.GetMembers();
            foreach (var item in members)
            {
                item.Roles = provider.Role.GetRolesByMember(item.MemberId);
            }
            return View(members);

        }
        public IActionResult Detail(string id)
        {
            Member member = provider.Member.GetMemberById(Guid.Parse(id));
            member.Roles = provider.Role.GetRolesByMember(member.MemberId);
            member.Contacts = provider.Contact.GetContactsByMember(member.MemberId);
            return View(member);
        }
        [HttpPost]
        public IActionResult AddRole(MemberInRole obj)
        {            
            return Json(provider.MemberInRole.Add(obj));
        }
        [HttpPost]
        public IActionResult BanAccount(Guid memberId)
        {
            PushNotification(new NotificationOption
            {
                Type = "success",
                Message = "Đã cập nhật trạng thái hoạt động của tài khoản."
            });            
            return Json(provider.Member.UpdateAccountStatus(memberId));
        }
        public IActionResult Search(string query)
        {
            IEnumerable<Member> members = provider.Member.Search(query);
            foreach (Member item in members)
            {
                item.Roles = provider.Role.GetRolesByMember(item.MemberId);
            }
            return Json(members);

        }
    }
}
