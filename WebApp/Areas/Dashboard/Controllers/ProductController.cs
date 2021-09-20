using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WebApp.Controllers;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class ProductController : BaseController
    {        
        public ProductController(SiteProvider provider) : base(provider)
        {
            
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
            product.Images = provider.ImageOfProduct.GetImagesByProduct(id);
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
            ImageOfProductUpload obj = JsonConvert.DeserializeObject<ImageOfProductUpload>(data);
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            int numberImageExists = provider.ImageOfProduct.GetNumberImageExists(obj);
            if (image != null && !string.IsNullOrEmpty(image.FileName))
            {
                string fileName = obj.Sku + "-" + obj.ColorId + "-" + (numberImageExists + 1) + Path.GetExtension(image.FileName);
                string path = Path.Combine(root, fileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                provider.ImageOfProduct.AddImageOfProduct(obj, fileName);
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
            List<ColorOfProduct> productColors = new List<ColorOfProduct>();
            foreach (var item in listColor)
            {
                productColors.Add(new ColorOfProduct
                {
                    ProductId = obj.ProductId,
                    ColorId = item
                });
            }
            List<SizeOfProduct> productSizes = new List<SizeOfProduct>();
            foreach (var item in listSize)
            {
                productSizes.Add(new SizeOfProduct
                {
                    ProductId = obj.ProductId,
                    SizeId = item
                });
            }

            List<GuideOfProduct> productGuides = new List<GuideOfProduct>();
            foreach (var item in listGuide)
            {
                productGuides.Add(new GuideOfProduct
                {
                    ProductId = obj.ProductId,
                    GuideId = item
                });
            }
            List<ProductInCategory> productCategories = new List<ProductInCategory>();
            foreach (var item in listCategory)
            {
                productCategories.Add(new ProductInCategory
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
            List<ColorOfProduct> productColors = new List<ColorOfProduct>();
            foreach (var item in listColor)
            {
                productColors.Add(new ColorOfProduct
                {
                    ProductId = obj.ProductId,
                    ColorId = item
                });
            }
            List<SizeOfProduct> productSizes = new List<SizeOfProduct>();
            foreach (var item in listSize)
            {
                productSizes.Add(new SizeOfProduct
                {
                    ProductId = obj.ProductId,
                    SizeId = item
                });
            }

            List<GuideOfProduct> productGuides = new List<GuideOfProduct>();
            foreach (var item in listGuide)
            {
                productGuides.Add(new GuideOfProduct
                {
                    ProductId = obj.ProductId,
                    GuideId = item
                });
            }
            List<ProductInCategory> productCategories = new List<ProductInCategory>();
            foreach (var item in listCategory)
            {
                productCategories.Add(new ProductInCategory
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
        public IActionResult DeleteImage(ImageOfProduct obj)
        {
            return Json(provider.ImageOfProduct.Delete(obj));
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
            provider.InventoryQuantity.UpdateInventoryQuantity(list);
            return Redirect($"/dashboard/product/detail/{productId}#quantityInInventory");
        }
    }
}

