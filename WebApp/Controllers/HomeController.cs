using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {       
        public HomeController(IRepositoryManager provider) :base(provider)
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
