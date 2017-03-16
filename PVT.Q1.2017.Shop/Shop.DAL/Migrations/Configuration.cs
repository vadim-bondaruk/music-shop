namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Context;

    using Shop.Common.Models;

    /// <summary>
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ShopContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ShopContext context)
        {
            // if (!context.Set<Currency>().Any(c => c.ShortName == "EUR"))
            // {
            // context.Set<Currency>().AddOrUpdate(new Currency { ShortName = "EUR", Code = 978, FullName = "EURO" });
            // }

            // if (!context.Set<Currency>().Any(c => c.ShortName == "USD"))
            // {
            // context.Set<Currency>()
            // .AddOrUpdate(new Currency { ShortName = "USD", Code = 840, FullName = "US Dollar" });
            // }

            // context.SaveChanges();

            // if (!context.Set<CurrencyRate>().Any())
            // {
            // context.Set<CurrencyRate>()
            // .AddOrUpdate(new CurrencyRate { CurrencyId = 1, TargetCurrencyId = 2, CrossCourse = 1.06M });
            // }

            // context.SaveChanges();

            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data. E.g.
            // context.People.AddOrUpdate(
            // p => p.FullName,
            // new Person { FullName = "Andrew Peters" },
            // new Person { FullName = "Brice Lambson" },
            // new Person { FullName = "Rowan Miller" }
            // );
        }
    }
}