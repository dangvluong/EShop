using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ImageOfProduct
    {
        public short ProductId { get; set; }
        public short ColorId { get; set; }
        public string ImageUrl { get; set; }
    }
}
