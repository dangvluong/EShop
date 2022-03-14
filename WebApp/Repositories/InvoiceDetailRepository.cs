using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class InvoiceDetailRepository : BaseRepository,IInvoiceDetailRepository
    {
        public InvoiceDetailRepository(IDbConnection connection) : base(connection) { }
        public IEnumerable<InvoiceDetail> GetInvoiceDetails(Guid invoiceId)
        {
            return connection.Query<InvoiceDetail>("GetInvoiceDetailByInvoiceId", new { InvoiceId = invoiceId }, commandType: CommandType.StoredProcedure);
        }
        public int GetTotalRevenue()
        {
            return connection.QuerySingleOrDefault<int>("SELECT SUM(Price *  Quantity) FROM InvoiceDetail");
        }
        public IEnumerable<Statistic> GetRevenueByMonths()
        {
            return connection.Query<Statistic>("GetRevenueByMonths", commandType: CommandType.StoredProcedure);
        }
    }
}
