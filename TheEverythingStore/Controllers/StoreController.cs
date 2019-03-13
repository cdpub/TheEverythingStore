using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEverythingStore.Models;

namespace TheEverythingStore.Controllers
{
    public class StoreController : Controller
    {
        //
        private DbModel db = new DbModel();

        // GET: Store index view
        public ActionResult Index()
        {
            //get list of categories
            var categories = db.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        //GET: Store/Products/5
        public ActionResult Products(int id)
        {
            var products = db.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
            return View(products);
        }

        //GET: Store/AddToCart/1003
        public ActionResult AddToCart(int id)
        {
            //var products = db.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
            return RedirectToAction("ShoppingCart");
        }
    }
}