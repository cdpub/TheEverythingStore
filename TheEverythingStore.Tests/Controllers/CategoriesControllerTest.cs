using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//add new references
using System.Web.Mvc;
using TheEverythingStore.Controllers;
using Moq; 
using TheEverythingStore.Models;
using System.Linq;  // for writing queries
using System.Collections.Generic;

namespace TheEverythingStore.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        /*  create a global controller instead of instantiating 
         *  in each method
        */

        //  moq data
        CategoriesController controller;
        //create a mock list of data
         List<Category> categories;
        //create mock object
        Mock<IMockCategories> mock;

        /*  create an initialize method that automatically runs
            before individual test 
        */
        [TestInitialize]
        public void TestInitialize()
        {
            // create some mock data
            categories = new List<Category>
            {
                new Category { CategoryId = 500, Name = "Fake Category One" },
                new Category { CategoryId = 501, Name = "Fake Category Two" },
                new Category { CategoryId = 502, Name = "Fake Category Three" }
            };

            // set up & populate our mock object to inject into our controller
            mock = new Mock<IMockCategories>();
            mock.Setup(c => c.Categories).Returns(categories.AsQueryable());

            // initialize the controller and inject the mock object
            controller = new CategoriesController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            // arrange
            // now handled in TestInitialize

            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexLoadCategories()
        {
            //act
            //call index method
            //access the data model returned to the view
            //cast the data as a list of category
            var results = (List<Category>) ((ViewResult)controller.Index()).Model;

            //assert
            CollectionAssert.AreEqual(categories.OrderBy(c => c.Name).ToList(), results);
        }
    }
}
