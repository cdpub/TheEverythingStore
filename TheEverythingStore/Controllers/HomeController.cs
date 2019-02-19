﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEverythingStore.Models;

namespace TheEverythingStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Products()
        {
            /* OLD UNTYPED VIEWBAG CODE
            var products = new List<string>();
            //create mock products
            for (int i = 1; i <= 10; i++) {

                products.Add("Product " + i.ToString());
            }

            //pass mock products to the view for display
            ViewBag.Products = products; */

            //create instances of product class
            var products = new List<Product>();

            //create a loop 
            for (int i = 1; i <= 10; i++)
            {
                //instantiate a new product object/ constructor
                Product product = new Product();

                //set name property
                product.Name = "Product " + i.ToString();

                //add the object to the list
                products.Add(product);
            }

            //load the view and pass the product list to it
            return View(products);
        }

        public ActionResult ViewProduct(string Productname)
        {
            //pass the string to the view using ViewBag since it is a single string value 
            //but can also use strongly type
            ViewBag.ProductName = Productname;
            return View();
        }
    }
}