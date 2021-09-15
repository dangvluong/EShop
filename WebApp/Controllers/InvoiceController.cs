using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        SiteProvider provider;
        public InvoiceController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Detail(Guid id)
        {
            Invoice obj = provider.Invoice.GetInvoiceById(id);
            obj.Contact = provider.Contact.GetContactsById(obj.ContactId);
            obj.InvoiceDetails = provider.InvoiceDetail.GetInvoiceDetails(id);
            return View(obj);
        }
        public IActionResult Index()
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<Invoice> invoices = provider.Invoice.GetInvoicesByMember(memberId);
            foreach (Invoice invoice in invoices)
            {
                invoice.Contact = provider.Contact.GetContactsById(invoice.ContactId);
                invoice.Member = provider.Member.GetMemberById(memberId);
            }
            return View(invoices);
        }
        public IActionResult InvoiceOfMember()
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<Invoice> invoices = provider.Invoice.GetInvoicesByMember(memberId);
            foreach (Invoice invoice in invoices)
            {
                invoice.Contact = provider.Contact.GetContactsById(invoice.ContactId);
                invoice.Member = provider.Member.GetMemberById(memberId);
            }
            return View(invoices);
        }
    }
}
