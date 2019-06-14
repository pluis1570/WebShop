using System.Linq;
using System.Web.Mvc;
using Webshop.Core.Models;
using Webshop.Core.ViewModels;
using WebShop.Core.Interfaces.Repositories;

namespace WebShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCatergory> ProductCategoryContext;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCatergory> productCategoryContext)
        {
            this.Context = productContext;
            this.ProductCategoryContext = productCategoryContext;
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = Context.Collection().ToList();
            return View(products);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ProductManagerViewModel viewmodel = new ProductManagerViewModel();
            viewmodel.Product = new Product();
            viewmodel.ProductCatergories = ProductCategoryContext.Collection();
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            return View();
            Context.Insert(product);
            Context.Commit();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var productToDelete = Context.FindById(id);
            return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var productToDelete = Context.Delete(Id);

            Context.Commit();
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
        public ActionResult Edit(string id)
        {
            //var productToUpdate = Context.FindProduct(id);
            var product = Context.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductManagerViewModel viewmodel = new ProductManagerViewModel();
            viewmodel.Product = product;
            viewmodel.ProductCatergories = ProductCategoryContext.Collection();
            return View(viewmodel);
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