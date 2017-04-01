namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.DAL.Context.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.ShopContext context)
        {
            AddDefaultCurrencies(context);
            AddDefaultCurrencyRates(context);
            AddDefaultGenres(context);
            AddDefaultPriceLevels(context);
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
                    CrossCourse = 0.9M
                }});

                context.Set<CurrencyRate>().AddOrUpdate(new[] { new CurrencyRate {
                    CurrencyId = 1,
                    TargetCurrencyId = 2,
                    CrossCourse = 1.2M
                }});
            }

            context.SaveChanges();
        }

        private void AddDefaultGenres(Context.ShopContext context)
        {
            if (!context.Set<Genre>().Any())
            {
                // source: https://en.wikipedia.org/wiki/List_of_popular_music_genres
                context.Set<Genre>().AddOrUpdate(new[]
                {
                    new Genre { Name = "Unknown" },
                    new Genre { Name = "Blues" },
                    new Genre { Name = "Country" },
                    new Genre { Name = "Electronic" },
                    new Genre { Name = "Folk" },
                    new Genre { Name = "Hip hop" },
                    new Genre { Name = "Jazz" },
                    new Genre { Name = "Latin" },
                    new Genre { Name = "Pop" },
                    new Genre { Name = "R&B and soul" },
                    new Genre { Name = "Rock" }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultPriceLevels(Context.ShopContext context)
        {
            if (!context.Set<PriceLevel>().Any())
            {
                context.Set<PriceLevel>().AddOrUpdate(new[] { new PriceLevel { Name = "Default" } });
                context.SaveChanges();
            }
        }
    }
}
