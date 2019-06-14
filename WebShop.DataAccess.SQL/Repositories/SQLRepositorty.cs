using System.Data.Entity;
using System.Linq;
using Webshop.Core.Models;
using WebShop.Core.Interfaces.Repositories;
using WebShop.DataAccess.SQL.Contexts;

namespace WebShop.DataAccess.SQL.Repositories
{
    public class SQLRepositorty<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbset;

        public SQLRepositorty(DataContext context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public bool Delete(string Id)
        {
            var t = FindById(Id);
            if (context.Entry(t). State == EntityState.Detached)
            {
                dbset.Attach(t);
            }
            context.Entry(t).State = EntityState.Deleted;
            dbset.Remove(t);

            return true;
        }

        public T FindById(string Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T t)
        {
            dbset.Add(t);
        }

        public void Update(T t)
        {
            dbset.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            Commit();
        }
    }
}
