﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : BaseController
    {        
        public CartController(IConfiguration configuration):base(configuration)
        {            
        }
        public IActionResult Index()
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                CartViewModel cart = new CartViewModel
                {
                    CartDetail = provider.Cart.GetCarts(Guid.Parse(cartId))
                };                
                foreach (var item in cart.CartDetail)
                {
                    item.AvailableQuantity = provider.InventoryQuantity.GetInventoryQuantitiesByProductColorAndSize(item.ProductId, item.ColorId, item.SizeId);
                }
                return View(cart);
            }
            return Redirect("/");
        }

        public IActionResult AddCart(Cart obj)
        {
            string cartId = Request.Cookies["cart"];
            if (string.IsNullOrEmpty(cartId))
            {
                CookieOptions options = new CookieOptions
                {
                    Path = "/",
                    Expires = DateTime.UtcNow.AddDays(30)
                };
                obj.CartId = Guid.NewGuid();
                Response.Cookies.Append("cart", obj.CartId.ToString(), options);
            }
            else
            {
                obj.CartId = Guid.Parse(cartId);
            }
            Product product = provider.Product.GetProductById(obj.ProductId);
            if (product.PriceSaleOff is null)
            {
                obj.Price = product.Price;
            }
            else
            {
                obj.Price = (int)product.PriceSaleOff;
            }
            provider.Cart.Add(obj);
            return Redirect("/cart");
        }
        [HttpPost]
        public IActionResult EditCart(Cart obj)
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                obj.CartId = Guid.Parse(cartId);
                return Json(provider.Cart.Edit(obj));
            }
            return Redirect("/");
        }

        public IActionResult DeleteCart(Cart obj)
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                obj.CartId = Guid.Parse(cartId);
                return Json(provider.Cart.Delete(obj));
            }
            return Redirect("/");
        }
        [Authorize]
        public IActionResult Checkout()
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Member member = provider.Member.GetMemberById(memberId);
                member.Contacts = provider.Contact.GetContactsByMember(memberId);
                CartViewModel cart = new CartViewModel
                {
                    CartDetail = provider.Cart.GetCarts(Guid.Parse(cartId))
                };
                foreach (var item in cart.CartDetail)
                {
                    item.AvailableQuantity = provider.InventoryQuantity.GetInventoryQuantitiesByProductColorAndSize(item.ProductId, item.ColorId, item.SizeId);
                }
                ViewBag.cart = cart;
                return View(member);
            }
            return Redirect("/");
        }
        [HttpPost, Authorize]
        public IActionResult Checkout(Invoice obj)
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId))
            {
                Guid memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                obj.MemberId = memberId;
                obj.CartId = Guid.Parse(cartId);
                obj.StatusId = (int)InvoiceStatus.Processing;
                obj.InvoiceId = Guid.NewGuid();
                obj.DateCreated = DateTime.UtcNow;
                provider.Invoice.Add(obj);
                return Redirect($"/invoice/detail/{obj.InvoiceId}");
            }
            return Redirect("/");
        }
    }
}
