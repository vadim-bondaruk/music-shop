namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Models;
    using System;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.DAL.Context.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context.ShopContext context)
        {
            AddDefaultCurrencies(context);
            AddDefaultCurrencyRates(context);
            AddDefaultGenre(context);
        }

        private void AddDefaultCurrencies(Context.ShopContext context)
        {
            if (!context.Set<Currency>().Any(c => c.ShortName == "EUR"))
            {
                context.Set<Currency>().AddOrUpdate(new[] { new Currency {
                    ShortName = "EUR",
                    Code = 978,
                    FullName = "EURO"
                }});
            }

            if (!context.Set<Currency>().Any(c => c.ShortName == "USD"))
            {
                context.Set<Currency>().AddOrUpdate(new[] { new Currency {
                    ShortName = "USD",
                    Code = 840,
                    FullName = "US Dollar"
                }});
            }

            context.SaveChanges();
        }

        private void AddDefaultCurrencyRates(Context.ShopContext context)
        {
      
            if (!context.Set<CurrencyRate>().Any())
            {
                context.Set<CurrencyRate>().AddOrUpdate(new[] { new CurrencyRate {
                    CurrencyId = 1,
                    TargetCurrencyId = 2,
                    CrossCourse = 1.06M,
                    Date=DateTime.Now
                }});
            }

            context.SaveChanges();
        }

        private void AddDefaultGenre(Context.ShopContext context)
        {
            if (!context.Set<Genre>().Any())
            {
                context.Set<Genre>().AddOrUpdate(new[] { new Genre { Name = "Unknown Genre" } });
                context.SaveChanges();
            }
        }
    }
}
