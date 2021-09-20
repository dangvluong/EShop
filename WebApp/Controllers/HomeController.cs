using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {       
        public HomeController(IConfiguration configuration):base(configuration)
        {            
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = provider.Product.GetRandom12Products();
            foreach (var item in products)
            {
                item.Images = provider.ImageOfProduct.GetImagesByProduct(item.ProductId);
            }
            return View(products);
        }
    }
}
