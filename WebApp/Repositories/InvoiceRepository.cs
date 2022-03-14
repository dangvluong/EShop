using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class InvoiceRepository : BaseRepository,IInvoiceRepository
    {
        public InvoiceRepository(IDbConnection connection) : base(connection) { }
        public int Add(Invoice obj)
        {
            return connection.Execute("AddInvoice", new
            {
                InvoiceId = obj.InvoiceId,
                MemberId = obj.MemberId,
                ContactId = obj.ContactId,
                StatusId = obj.StatusId,
                DateCreated = obj.DateCreated,
                CartId = obj.CartId,
                ShipCost = obj.ShipCost
            }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Invoice> GetInvoicesByMember(Guid memberId)
        {
            return connection.Query<Invoice>("GetInvoicesByMember", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);
        }
        public Invoice GetInvoiceById(Guid invoiceId)
        {
            return connection.QuerySingleOrDefault<Invoice>("GetInvoiceById", new { InvoiceId = invoiceId }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Invoice> GetInvoices()
        {
            return connection.Query<Invoice>("SELECT * FROM Invoice");
        }
        public int UpdateStatus(Invoice obj)
        {
            return connection.Execute("UpdateInvoiceStatus", new {InvoiceId = obj.InvoiceId, StatusId = obj.StatusId }, commandType: CommandType.StoredProcedure);
        }
    }
}
