using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Core.Models;
using WebShop.DataAccess.InMemory.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public ProductRepository Context { get; set; } = new ProductRepository();

        // GET: Product
        public ActionResult Index()
        {
            var products = Context.Collection().ToList();

            return View(products);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var product = new Product();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
                return View();

            Context.Insert(product);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var productToDelete = Context.FindProduct(id);
            return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var productToDelete = Context.Delete(Id);
            if (productToDelete == false)
            {
                return HttpNotFound();

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit (string id)
        {
            var productToUpdate = Context.FindProduct(id);

            return View(productToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Context.Update(product);
            Context.Commit();
            return RedirectToAction("Index");
        }
    }
}