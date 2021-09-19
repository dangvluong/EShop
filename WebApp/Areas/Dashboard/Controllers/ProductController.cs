﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class ProductController : Controller
    {
        SiteProvider provider;        
        public ProductController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index(int id = 1)
        {
            IEnumerable<Product> products = provider.Product.GetAllProduct();
            foreach (var item in products)
            {
                item.Sizes = provider.Size.GetSizesByProduct(item.ProductId);
                item.Colors = provider.Color.GetColorsByProduct(item.ProductId);
            }
            return View(products);
        }
        public IActionResult Detail(short id)
        {
            Product product = provider.Product.GetProductById(id);
            product.ProductImages = provider.ProductImage.GetImagesByProduct(id);
            product.Colors = provider.Color.GetColorsByProduct(id);
            product.Categories = provider.Category.GetCategoriesByProduct(id);
            product.Sizes = provider.Size.GetSizesByProduct(id);
            product.Guides = provider.Guide.GetGuidesByProduct(id);
            ViewBag.categories = provider.Category.GetCategories();
            return View(product);
        }
        [HttpPost]
        public IActionResult ImageUpload(IFormFile image, string data)
        {
            ProductImageUpload obj = JsonConvert.DeserializeObject<ProductImageUpload>(data);
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            int numberImageExists = provider.ProductImage.GetNumberImageExists(obj);
            if (image != null && !string.IsNullOrEmpty(image.FileName))
            {
                string fileName = obj.Sku + "-" + obj.ColorId + "-" + (numberImageExists + 1) + Path.GetExtension(image.FileName);
                string path = Path.Combine(root, fileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                provider.ProductImage.AddProductImage(obj, fileName);
                return Json(fileName);
            }
            return null;
        }
        public IActionResult Edit(short id)
        {
            Product product = provider.Product.GetProductById(id);
            product.Colors = provider.Color.GetColorsByProduct(id);
            product.Sizes = provider.Size.GetSizesByProduct(id);
            product.Guides = provider.Guide.GetGuidesByProduct(id);
            product.Categories = provider.Category.GetCategoriesByProduct(id);
            ViewBag.sizes = provider.Size.GetSizes();
            ViewBag.colors = provider.Color.GetColors();
            ViewBag.guides = provider.Guide.GetGuids();
            ViewBag.categories = provider.Category.GetCategories();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product obj, short[] listColor, short[] listSize, short[] listGuide, short[] listCategory)
        {
            provider.Product.Edit(obj);
            List<ProductColor> productColors = new List<ProductColor>();
            foreach (var item in listColor)
            {
                productColors.Add(new ProductColor
                {
                    ProductId = obj.ProductId,
                    ColorId = item
                });
            }
            List<ProductSize> productSizes = new List<ProductSize>();
            foreach (var item in listSize)
            {
                productSizes.Add(new ProductSize
                {
                    ProductId = obj.ProductId,
                    SizeId = (byte)item
                });
            }

            List<ProductGuide> productGuides = new List<ProductGuide>();
            foreach (var item in listGuide)
            {
                productGuides.Add(new ProductGuide
                {
                    ProductId = obj.ProductId,
                    GuideId = item
                });
            }
            List<ProductCategory> productCategories = new List<ProductCategory>();
            foreach (var item in listCategory)
            {
                productCategories.Add(new ProductCategory
                {
                    ProductId = obj.ProductId,
                    CategoryId = item
                });
            }
            provider.ProductCategory.Edit(productCategories);
            provider.ProductColor.Edit(productColors);
            provider.ProductGuide.Edit(productGuides);
            provider.ProductSize.Edit(productSizes);
            return Redirect($"/dashboard/product/detail/{obj.ProductId}");
        }
        public IActionResult Add()
        {
            ViewBag.sizes = provider.Size.GetSizes();
            ViewBag.colors = provider.Color.GetColors();
            ViewBag.guides = provider.Guide.GetGuids();
            ViewBag.categories = provider.Category.GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product obj, short[] listColor, short[] listSize, short[] listGuide, short[] listCategory)
        {
            obj.ProductId = provider.Product.Add(obj);
            List<ProductColor> productColors = new List<ProductColor>();
            foreach (var item in listColor)
            {
                productColors.Add(new ProductColor
                {
                    ProductId = obj.ProductId,
                    ColorId = item
                });
            }
            List<ProductSize> productSizes = new List<ProductSize>();
            foreach (var item in listSize)
            {
                productSizes.Add(new ProductSize
                {
                    ProductId = obj.ProductId,
                    SizeId = (byte)item
                });
            }

            List<ProductGuide> productGuides = new List<ProductGuide>();
            foreach (var item in listGuide)
            {
                productGuides.Add(new ProductGuide
                {
                    ProductId = obj.ProductId,
                    GuideId = item
                });
            }
            List<ProductCategory> productCategories = new List<ProductCategory>();
            foreach (var item in listCategory)
            {
                productCategories.Add(new ProductCategory
                {
                    ProductId = obj.ProductId,
                    CategoryId = item
                });
            }
            provider.ProductCategory.Add(productCategories);
            provider.ProductColor.Add(productColors);
            provider.ProductGuide.Add(productGuides);
            provider.ProductSize.Add(productSizes);
            return Redirect($"/dashboard/product/detail/{obj.ProductId}");
        }
        public IActionResult Delete(short id)
        {
            int result = provider.Product.Delete(id);
            string[] msg = { "Lỗi", "Xóa thành công" };
            result = result > 1 ? 1 : result;
            TempData["msg"] = msg[result];
            return Redirect("/dashboard/product");

        }
        public IActionResult DeleteImage(ProductImage obj)
        {
            return Json(provider.ProductImage.Delete(obj));
        }
        public IActionResult UpdateQuantity(short id)
        {
            Product product = provider.Product.GetProductById(id);
            product.Sizes = provider.Size.GetSizesByProduct(id);
            product.Colors = provider.Color.GetColorsByProduct(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(List<InventoryQuantity> list)
        {
            short productId = list[0].ProductId;
            provider.InventoryStatus.UpdateInventoryQuantity(list);
            return Redirect($"/dashboard/product/detail/{productId}#quantityInInventory");
        }
    }
}

