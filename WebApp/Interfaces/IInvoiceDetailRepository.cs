using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        IEnumerable<InvoiceDetail> GetInvoiceDetails(Guid invoiceId);
        int GetTotalRevenue();
        IEnumerable<Statistic> GetRevenueByMonths();
    }
}
