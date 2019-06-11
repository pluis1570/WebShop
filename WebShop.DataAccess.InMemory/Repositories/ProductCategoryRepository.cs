using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Webshop.Core.Models;

namespace WebShop.DataAccess.InMemory.Repositories
{
    public class ProductCategoryRepository
    {

        ObjectCache Cache = MemoryCache.Default;
        List<ProductCatergory> ProductCategories;

        public ProductCategoryRepository()
        {

            ProductCategories = (List<ProductCatergory>)Cache["productsCategories"];

            if (ProductCategories == null)
                ProductCategories = new List<ProductCatergory>();
        }



        public void Commit()
        {
            Cache["productsCategories"] = ProductCategories;
        }

        public void Insert(ProductCatergory product)
        {
            ProductCategories.Add(product);
            Commit();
        }

        public void Update(ProductCatergory product)
        {
            ProductCatergory productToUpdate = ProductCategories.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                int index = ProductCategories.FindIndex(p => p.Id == productToUpdate.Id);
                ProductCategories[index] = product;
            }
            else
            {
                throw new Exception("product not found");
            }
        }

        public ProductCatergory FindProduct(string Id)
        {
            var tempProduct = ProductCategories.Find(x => x.Id == Id);

            if (tempProduct != null)
                return tempProduct;
            else
                throw new Exception("404 Product Not Found");
        }

        public IQueryable<ProductCatergory> Collection()
        {
            return ProductCategories.AsQueryable();
        }

        public bool Delete(string id)
        {
            var tempProduct = FindProduct(id);
            var removed = ProductCategories.Remove(tempProduct);

            Commit();
            return (removed);
        }
    }
}
