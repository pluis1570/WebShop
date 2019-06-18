using System.Data.Entity;
using Webshop.Core.Models;

namespace WebShop.DataAccess.SQL.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext():base("Dataconnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatergory> Categories{ get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

    }
}
