using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Webshop.Core.Models;

namespace WebShop.DataAccess.InMemory.Repositories
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> Products;

        public ProductRepository()
        {
            Products = (List<Product>)Cache["products"];

            if (Products == null)
                Products = new List<Product>();
        }

        public void Commit()
        {
            Cache["products"] = Products;
        }

        public void Insert(Product product)
        {
            Products.Add(product);
            Commit();
        }

        public void Update(Product product)
        {
            var tempProduct = FindProduct(product.Id);
            Commit();
        }

        public Product FindProduct(string Id)
        {
            var tempProduct = Products.Find(x => x.Id == Id);

            if (tempProduct != null)
                return tempProduct;
            else
                throw new Exception("404 Product Not Found");
        }

        public IQueryable<Product> Collection()
        {
            return Products.AsQueryable();
        }

        public void Delete(string id)
        {
            var tempProduct = FindProduct(id);
            Products.Remove(tempProduct);
            Commit();
        }
    }
}
