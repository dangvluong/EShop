using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        SiteProvider provider;
        public CartController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            string cartId = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cartId)){
                IEnumerable<Cart> cart = provider.Cart.GetCarts(Guid.Parse(cartId));
                foreach (var item in cart)
                {
                    item.AvailableQuantity = provider.InventoryStatus.GetInventoryStatusByProductColorAndSize(item.ProductId, item.ColorId, item.SizeId);
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
    }
}
