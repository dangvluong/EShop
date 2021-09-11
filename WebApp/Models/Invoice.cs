using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public short ContactId { get; set; }
        public Contact Contact { get; set; }
        public byte StatusId { get; set; }
        public Guid CartId { get; set; }
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }

    }
}
