using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IInvoiceRepository
    {
        int Add(Invoice obj);
        IEnumerable<Invoice> GetInvoicesByMember(Guid memberId);
        Invoice GetInvoiceById(Guid invoiceId);
        IEnumerable<Invoice> GetInvoices();
        int UpdateStatus(Invoice obj);
    }
}
