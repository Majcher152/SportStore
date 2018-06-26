using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            //Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            //Cart cart = JsonConvert.DeserializeObject<T>(HttpContext.Session.GetString("Cart"));
            Cart cart = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Cart")) as Cart;
            return cart;
        }

        private void SaveCart(Cart cart)
        {

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }

    }
}