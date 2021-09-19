using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class InventoryQuantity
    {
        public short ProductId { get; set; }        
        public short ColorId { get; set; }
        public byte SizeId { get; set; }
        public short Quantity { get; set; }
    }
}
