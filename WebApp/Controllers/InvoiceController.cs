using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class InvoiceController : BaseController
    {        
        public InvoiceController(SiteProvider provider) :base(provider)
        {          
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
            }
            ViewBag.member = provider.Member.GetMemberById(memberId);
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
