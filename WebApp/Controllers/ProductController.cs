using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : BaseController
    {       
        int size = 20;
        public ProductController(IRepositoryManager provider) :base(provider)
        {
            
        }
        public IActionResult Index(int id = 1)
        {
            IEnumerable<Product> products = provider.Product.GetProducts(id, size, out int total);
            if(products == null)
                return NotFound();
            foreach (var item in products)
            {
                item.Images = provider.ImageOfProduct.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(products);
        }

        public IActionResult Detail(short id)
        {
            Product product = provider.Product.GetProductById(id);
            if(product == null)
                return NotFound();
            product.Images = provider.ImageOfProduct.GetImagesByProduct(id);
            product.Colors = provider.Color.GetColorsByProduct(id);
            product.Categories = provider.Category.GetCategoriesByProduct(id);
            product.Sizes = provider.Size.GetSizesByProduct(id);            
            product.Guides = provider.Guide.GetGuidesByProduct(id);
            product.ProductsRelation = provider.Product.GetProductsRelation(id);
            foreach (var item in product.ProductsRelation)
            {
                item.Images = provider.ImageOfProduct.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categories = provider.Category.GetCategories();
            return View(product);
        }
        [Route("/product/category/{id}/{p?}")]
        public IActionResult Category(short id, int p = 1)
        {
            IEnumerable<Product> productsByCategory = provider.Product.GetProductsByCategory(id, p, size, out int total);
            if (productsByCategory == null)
                return NotFound();
            foreach (var item in productsByCategory)
            {
                item.Images = provider.ImageOfProduct.GetImagesByProduct(item.ProductId);
            }
            ViewBag.categoryId = id;
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);
            return View(productsByCategory);
        }       
    
        public IActionResult Search( string query, int id = 1)
        {
            ICollection<Product> searchProducts = provider.Product.SearchProduct(query, id, size, out int total);
            foreach (var item in searchProducts)
            {
                item.Images = provider.ImageOfProduct.GetImagesByProduct(item.ProductId);
            }            
            ViewBag.categories = provider.Category.GetCategories();
            ViewBag.totalPage = (int)Math.Ceiling(total / (float)size);            
            return View(searchProducts);
        }

        [HttpPost]
        public int GetInventoryQuantity(short productId, short colorId, short sizeId)
        {
            return provider.InventoryQuantity.GetInventoryQuantityByProductColorAndSize(productId, colorId, sizeId);
        }
    }
}
