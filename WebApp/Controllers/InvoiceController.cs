using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class InvoiceController : Controller
    {
        SiteProvider provider;
        public InvoiceController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            return Redirect("/");
        }
        public IActionResult Detail(Guid id)
        {
            Invoice obj = provider.Invoice.GetInvoiceById(id);
            obj.Contact = provider.Contact.GetContactsById(obj.ContactId);
            obj.InvoiceDetails = provider.InvoiceDetail.GetInvoiceDetails(id);
            return View(obj);
        }
    }
}
