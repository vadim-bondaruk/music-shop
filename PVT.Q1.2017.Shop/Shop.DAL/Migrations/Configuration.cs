namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using Infrastructure.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.DAL.Context.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context.ShopContext context)
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

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
