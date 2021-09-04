using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        SiteProvider provider;
        int size = 20;
        public HomeController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index(int id = 1)
        {
            IEnumerable<Product> products = provider.Product.GetProducts(id, size, out int total);
            foreach (var item in products)
            {
                item.ProductImages = provider.ProductImage.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(products);
        }

        public IActionResult Detail(short id)
        {
            Product product = provider.Product.GetProductById(id);
            product.ProductImages = provider.ProductImage.GetImagesByProduct(id);
            product.ProductColor = provider.Color.GetColorsByProduct(id);
            product.Categories = provider.Category.GetCategoriesByProduct(id);
            product.Sizes = provider.Size.GetSizesByProduct(id);
            //product.InventoryStatuses = inventoryStatusRepository.GetInventoryStatusesByProduct(id);
            product.Guides = provider.Guide.GetGuidesByProduct(id);
            ViewBag.categories = provider.Category.GetCategories();
            return View(product);
        }
        [Route("/home/category/{id}/{p?}")]
        public IActionResult Category(short id, int p = 1)
        {
            IEnumerable<Product> productsByCategory = provider.Product.GetProductsByCategory(id, p, size, out int total);
            foreach (var item in productsByCategory)
            {
                item.ProductImages = provider.ProductImage.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categoryId = id;
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(productsByCategory);
        }       
    
        public IActionResult Search( string query, int id = 1)
        {
            IEnumerable<Product> searchProducts = provider.Product.SearchProduct(query, id, size, out int total);
            foreach (var item in searchProducts)
            {
                item.ProductImages = provider.ProductImage.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(searchProducts);
        }

        [HttpPost]
        public int GetInventoryStatus(short productId, short colorId, byte sizeId)
        {
            return provider.InventoryStatus.GetInventoryStatusByProductColorAndSize(productId, colorId, sizeId);
        }
    }
}
