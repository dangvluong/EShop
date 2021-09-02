﻿using Microsoft.AspNetCore.Mvc;
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
        public HomeController(IConfiguration configuration)
        {
            productRepository= new ProductRepository(configuration);
            productImageRepository = new ProductImageRepository(configuration);
            colorRepository = new ColorRepository(configuration);
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = productRepository.GetProducts();
            foreach (var item in products)
            {
                item.ProductImages = productImageRepository.GetImagesByProduct(item.ProductId);
            }
            return View(products);
        }

        public IActionResult Detail(short id)
        {
            Product product = productRepository.GetProductById(id);
            product.ProductImages = productImageRepository.GetImagesByProduct(id);
            product.ProductColor = colorRepository.GetColorsByProduct(id);
            return View(product);
        }
    }
}
