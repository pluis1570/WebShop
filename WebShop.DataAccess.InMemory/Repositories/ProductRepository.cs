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
            Product productToUpdate = Products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                int index = Products.FindIndex(p => p.Id == productToUpdate.Id);
                Products[index] = product;
            }
            else
            {
                throw new Exception("product not found");
            }
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

        public bool Delete(string id)
        {
            var tempProduct = FindProduct(id);
            var removed = Products.Remove(tempProduct);

            Commit();
            return (removed);
        }
    }
}
