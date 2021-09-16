using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductImageUpload
    {
        public short ProductId { get; set; }
        public short ColorId { get; set; }
        public string Sku { get; set; }       
    }
}
