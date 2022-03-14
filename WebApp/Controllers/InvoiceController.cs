using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebApp.Helper;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class InvoiceController : BaseController
    {        
        public InvoiceController(IRepositoryManager provider) :base(provider)
        {          
        }
        public IActionResult Detail(Guid id)
        {
            Invoice obj = provider.Invoice.GetInvoiceById(id);
            obj.Contact = provider.Contact.GetContactById(obj.ContactId);
            obj.InvoiceDetails = provider.InvoiceDetail.GetInvoiceDetails(id);
            return View(obj);
        }
        public IActionResult Index()
        {
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IEnumerable<Invoice> invoices = provider.Invoice.GetInvoicesByMember(memberId);
            
            foreach (Invoice invoice in invoices)
            {
                invoice.Contact = provider.Contact.GetContactById(invoice.ContactId);                
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
                invoice.Contact = provider.Contact.GetContactById(invoice.ContactId);
                invoice.Member = provider.Member.GetMemberById(memberId);
            }
            return View(invoices);
        }
        public IActionResult CancelInvoice(Guid id)
        {
            Invoice invoice = provider.Invoice.GetInvoiceById(id);
            if (invoice == null)
                return BadRequest();
            Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if(invoice.MemberId != memberId)
                return Unauthorized();
            invoice.StatusId = (byte)InvoiceStatus.Cancel;
            int result = provider.Invoice.UpdateStatus(invoice);
            TempData["msg"] = $"Đã hủy đơn đặt hàng {id}";
            return RedirectToAction(nameof(Index));
        }
    }
}
