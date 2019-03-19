﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEverythingStore.Models;

namespace TheEverythingStore.Controllers
{
    public class StoreController : Controller
    {
        // db
        private DbModel db = new DbModel();

        // GET: Store
        public ActionResult Index()
        {
            var categories = db.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        // GET: Store/Products/5
        public ActionResult Products(int id)
        {
            var products = db.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
            return View(products);
        }

        // GET: Store/AddToCart/1003
        public ActionResult AddToCart(int id)
        {
            // identify the user
            GetCartUsername();

            // create new cart object
            Cart cart = new Cart();
            Product product = db.Products.SingleOrDefault(p => p.ProductId == id);

            cart.ProductId = id;
            cart.Quantity = 1;
            cart.Price = product.Price;
            cart.Username = Session["CartUsername"].ToString();

            // save to db
            db.Carts.Add(cart);
            db.SaveChanges();

            return RedirectToAction("ShoppingCart");
        }

        private void GetCartUsername()
        {
            // is there already a current cart Id for this session
            if (Session["CartUsername"] == null)
            {
                // is user logged in?
                if (User.Identity.Name == "")
                {
                    Session["CartUsername"] = Guid.NewGuid();
                }
                else
                {
                    Session["CartUsername"] = User.Identity.Name;
                }
            }
        }

        // GET: Store/ShoppingCart
        public ActionResult ShoppingCart()
        {
            // get the current user's cart items
            GetCartUsername();
            String Username = Session["CartUsername"].ToString();

            var cartItems = db.Carts.Where(c => c.Username == Username).ToList();
            return View(cartItems);
        }

    }
}