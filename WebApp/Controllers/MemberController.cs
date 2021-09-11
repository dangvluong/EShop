using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace WebApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        SiteProvider provider;
        public MemberController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Member member = provider.Member.GetMemberById(memberId);
            member.MemberId = memberId;
            member.Contacts = provider.Contact.GetContactsByMember(memberId);
            return View(member);
        }

        public IActionResult Manage()
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
    }
}
