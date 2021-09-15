using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("dashboard")]
    public class MemberController : Controller
    {
        SiteProvider provider;
        public MemberController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
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
