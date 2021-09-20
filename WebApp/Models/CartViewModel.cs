using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CartViewModel
    {
        public ICollection<Cart> CartDetail { get; set; }
        public int Total { get; set; }

    }
}
