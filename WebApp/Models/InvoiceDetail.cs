using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class InvoiceDetail
    {
        public Guid InvoiceId { get; set; }
        public short ProductId { get; set; }
        public string ProductName { get; set; }
        public short ColorId { get; set; }
        public string ColorCode { get; set; }
        public byte SizeId { get; set; }
        public string SizeCode { get; set; }
        public short Quantity { get; set; }
        public int Price { get; set; }
    }
}
