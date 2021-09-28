using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {

        public MemberController(SiteProvider provider) : base(provider)
        {

        }
        public IActionResult Index()
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Member member = provider.Member.GetMemberById(memberId);
            member.MemberId = memberId;
            member.Contacts = provider.Contact.GetContactsByMember(memberId);
            return View(member);
        }
    }
}
