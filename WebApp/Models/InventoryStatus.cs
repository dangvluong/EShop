using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class InventoryStatus
    {
        public byte SizeId { get; set; }
        public short ColorId { get; set; }
        public short Quantity { get; set; }
    }
}
