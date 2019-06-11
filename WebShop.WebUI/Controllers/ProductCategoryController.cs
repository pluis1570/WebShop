using System.Linq;
using System.Web.Mvc;
using Webshop.Core.Models;
using WebShop.DataAccess.InMemory.Repositories;



namespace WebShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        
        public ProductCategoryRepository Context { get; set; } = new ProductCategoryRepository();

        // GET: Product
        public ActionResult Index()
        {
            var ProductCategories = Context.Collection().ToList();

            return View(ProductCategories);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var ProductCategories = new ProductCatergory();
            return View();
        }


        [HttpPost]
        public ActionResult Create(ProductCatergory ProductCategories)
        {

            if (!ModelState.IsValid)
                return View();

            Context.Insert(ProductCategories);

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
        public ActionResult Edit(string id)
        {
            var productToUpdate = Context.FindProduct(id);

            return View(productToUpdate);
        }

        [HttpPost]

        public ActionResult Edit(ProductCatergory ProductCategories)
        {
            Context.Update(ProductCategories);
            Context.Commit();
            return RedirectToAction("Index");
        }
    }
}