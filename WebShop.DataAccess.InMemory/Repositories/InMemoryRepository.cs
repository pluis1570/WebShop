using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Webshop.Core.Models;
using WebShop.Core.Interfaces.Repositories;

namespace WebShop.DataAccess.InMemory.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T:BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }

        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
           var index = items.FindIndex( i => i.Id == t.Id);

            if (index == -1)
            {
           
                throw new Exception(className + " Not found");
            }

            items[index] = t;
        }

        public T FindById (String Id)
        {
            T t = items.Find(i => i.Id == Id);

            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }
            public IQueryable<T> Collection()
            {
                return items.AsQueryable();

            }

        public bool Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
               return items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }
    }
}
