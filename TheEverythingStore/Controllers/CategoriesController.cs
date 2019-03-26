using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq; // for sorting
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheEverythingStore.Models;

namespace TheEverythingStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        //private DbModel db = new DbModel();

        /*  modify DbModel to allow dependency injection, 
            if controller receives mock data, goes to mock repository
            if controller receives no data object, goes to database model
            first install moq for handling mock data; 
            move DBModel to Idata repository 

            then,  create 2 constructors - default constructor for db connection
            constructor with fake params for testing

            then, in models, create an interface class for two reposities
            one for mock and one for actual db data that inherits mock repository
        
            refactor save (find), edit (find) and delete 
        */

        IMockCategories db;

        // constructors
        // default constructor: no input params => use SQL Server & Entity Framework
        public CategoriesController()
        {
            this.db = new IDataCategories();
        }
        //constructor with fake params; return mock db
        public CategoriesController(IMockCategories mockDb)
        {
            this.db = mockDb;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.OrderBy(c => c.Name).ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //refactor find with SingleorDefault
            //Category category = db.Categories.Find(id);
             Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                //modify these with new save method in IDataCategories
                //db.Categories.Add(category);
                //db.SaveChanges();
                db.Save(category);
      
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //refactor find with SingleorDefault
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                //modify these with new save method in IDataCategories
                //db.Entry(category).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(category);

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //refactor find with SingleorDefault
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //refactor find with SingleorDefault
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);

            //modify and replace with IDataCategories Delete
            //db.Categories.Remove(category);
            //db.SaveChanges();
            db.Delete(category);
 
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
