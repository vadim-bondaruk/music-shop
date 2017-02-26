using System.Data.Entity;
using Shop.DAL.Migrations;

namespace Shop.DAL.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("ShopConnection")
        {
        }
    }
}