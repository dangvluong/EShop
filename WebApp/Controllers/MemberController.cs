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
            Guid memberId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Member member = provider.Member.GetMemberById(memberId);
            member.MemberId = memberId;
            member.Contacts = provider.Contact.GetContactsByMember(memberId);
            return View(member);
        }
        public IActionResult GetMembers()
        {
            return View();
        }
        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(Contact obj)
        {
            Guid memberId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            provider.Contact.AddContact(obj, memberId);
            return Redirect("/member");
        }

        [HttpPost]
        public IActionResult GetContactsByMember(Guid memberId)
        {
            Console.WriteLine(memberId);
            return Json(provider.Contact.GetContactsByMember(memberId));
        }

    }
}
