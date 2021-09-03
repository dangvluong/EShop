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
        ProductRepository productRepository;
        ProductImageRepository productImageRepository;
        ColorRepository colorRepository;
        CategoryRepository categoryRepository;
        int size = 20;
        public HomeController(IConfiguration configuration)
        {
            productRepository = new ProductRepository(configuration);
            productImageRepository = new ProductImageRepository(configuration);
            colorRepository = new ColorRepository(configuration);
            categoryRepository = new CategoryRepository(configuration);
        }
        public IActionResult Index(int id = 1)
        {
            IEnumerable<Product> products = productRepository.GetProducts(id, size, out int total);
            foreach (var item in products)
            {
                item.ProductImages = productImageRepository.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = categoryRepository.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(products);
        }

        public IActionResult Detail(short id)
        {
            Product product = productRepository.GetProductById(id);
            product.ProductImages = productImageRepository.GetImagesByProduct(id);
            product.ProductColor = colorRepository.GetColorsByProduct(id);
            product.Categories = categoryRepository.GetCategoriesByProduct(id);
            ViewBag.categories = categoryRepository.GetCategories();
            return View(product);
        }
        [Route("/home/category/{id}/{p?}")]
        public IActionResult Category(short id, int p = 1)
        {
            IEnumerable<Product> productsByCategory = productRepository.GetProductsByCategory(id, p, size, out int total);
            foreach (var item in productsByCategory)
            {
                item.ProductImages = productImageRepository.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categoryId = id;
            ViewBag.categories = categoryRepository.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(productsByCategory);
        }       
    
        public IActionResult Search( string query, int id = 1)
        {
            IEnumerable<Product> searchProducts = productRepository.SearchProduct(query, id, size, out int total);
            foreach (var item in searchProducts)
            {
                item.ProductImages = productImageRepository.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = categoryRepository.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(searchProducts);
        }
    }
}
