using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Core.Models;
using WebShop.Core.Interfaces.Repositories;
using Webshop.Core.ViewModels;


namespace WebShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCatergory> ProductCategoryContext;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCatergory> productCategoryContext)
        {
            this.Context = productContext;
            this.ProductCategoryContext = productCategoryContext;
        }


        public ActionResult Index(string category = null)
        {
            var productCategories = ProductCategoryContext.Collection().ToList();
            var products = Context.Collection().ToList();

            if (category == null)
                products = Context.Collection().ToList();
            else
                products = Context.Collection().Where(x => x.Category == category).ToList();

            var viewModel = new ProductListViewModel();

            viewModel.Categories = productCategories;
            viewModel.Products = products;

            return View(viewModel);
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

        public ActionResult Details(string  id)
        {
            var product = Context.FindById(id);
            if (product == null)
            {
                return HttpNotFound();

            }
            return View(product);
        }
    }
}