using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Product
    {
        public short ProductId { get; set; }
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public int Price { get; set; }
        public int? PriceSaleOff { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Color> ProductColor { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        //public IEnumerable<InventoryStatus> InventoryStatuses { get; set; }
        public IEnumerable<Guide> Guides { get; set; }
    }
}
