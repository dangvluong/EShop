using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        SiteProvider provider;
        public HomeController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = provider.Product.GetRandom12Products();
            foreach (var item in products)
            {
                item.ProductImages = provider.ProductImage.GetImagesByProduct(item.ProductId);
            }
            return View(products);
        }
    }
}
