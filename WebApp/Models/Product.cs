using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Product
    {
        public short ProductId { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
        [Display(Name = "Sku")]
        public string Sku { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]     
        [Display(Name = "Giá sản phẩm")]
        public int Price { get; set; }
        public int? PriceSaleOff { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
        [Display(Name = "Chất liệu")]
        public string Material { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập {0}")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; }
        public List<ImageOfProduct> Images { get; set; }
        public List<Color> Colors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<Size> Sizes { get; set; }               
        public IEnumerable<Guide> Guides { get; set; }        
        public List<Product> ProductsRelation { get; set; }
    }
}
