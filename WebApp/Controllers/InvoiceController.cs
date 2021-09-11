using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Index()
        {
            IEnumerable<Invoice> invoices = provider.Invoice.GetInvoices();
            foreach (Invoice invoice in invoices)
            {
                invoice.Contact = provider.Contact.GetContactsById(invoice.ContactId);
                invoice.Member = provider.Member.GetMemberById(invoice.MemberId);
            }
            return View(invoices);
        }
        public IActionResult Detail(Guid id)
        {
            Invoice obj = provider.Invoice.GetInvoiceById(id);
            obj.Contact = provider.Contact.GetContactsById(obj.ContactId);
            obj.InvoiceDetails = provider.InvoiceDetail.GetInvoiceDetails(id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult UpdateStatus(Invoice obj)
        {
            return Json(provider.Invoice.UpdateStatus(obj));            
        }
    }
}
