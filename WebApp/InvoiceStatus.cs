using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public enum InvoiceStatus
    {
        Processing = 1,
        Approved = 2,
        Shipping = 3,
        Success = 4,
        Cancel = 0
    }
}
