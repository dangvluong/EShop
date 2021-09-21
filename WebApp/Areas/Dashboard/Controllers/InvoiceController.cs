using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Controllers;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.Manage.Controllers
{
    [Area("dashboard")]
    [Authorize(Roles = "Manager, Staff")]
    public class InvoiceController : BaseController
    {
        public InvoiceController(SiteProvider provider) :base(provider)
        {
            
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
            obj.Member = provider.Member.GetMemberById(obj.MemberId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult UpdateStatus(Invoice obj)
        {
            if(obj.StatusId == 2)
            {
                obj.InvoiceDetails = provider.InvoiceDetail.GetInvoiceDetails(obj.InvoiceId);
                foreach (InvoiceDetail item in obj.InvoiceDetails)
                {
                    provider.InventoryQuantity.UpdateInventoryQuantity(item);
                }
            }
            return Json(provider.Invoice.UpdateStatus(obj));
        }
    }
}
