namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Shop.Common.Models;
    using Shop.DAL.Context;

    /// <summary>
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ShopContext>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        protected override void Seed(ShopContext context)
        {
            this.AddDefaultCurrencies(context);
            this.AddDefaultCurrencyRates(context);
            this.AddDefaultGenre(context);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void AddDefaultCurrencies(ShopContext context)
        {
            if (!context.Set<Currency>().Any(c => c.ShortName == "EUR"))
            {
                context.Set<Currency>().AddOrUpdate(new Currency { ShortName = "EUR", Code = 978, FullName = "EURO" });
            }

            // if (!context.Set<Currency>().Any(c => c.ShortName == "USD"))
            // {
            // context.Set<Currency>()
            // .AddOrUpdate(new Currency { ShortName = "USD", Code = 840, FullName = "US Dollar" });
            // }
            context.SaveChanges();
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void AddDefaultCurrencyRates(ShopContext context)
        {
            // Õ≈ –¿¡Œ“¿≈“ “. . Date - ÌÂ Nullable ÚËÔ!!!
            /*if (!context.Set<CurrencyRate>().Any())
            {
                context.Set<CurrencyRate>().AddOrUpdate(new[] { new CurrencyRate {
                    CurrencyId = 1,
                    TargetCurrencyId = 2,
                    CrossCourse = 1.06M
                }});
            }

            context.SaveChanges();*/
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void AddDefaultGenre(ShopContext context)
        {
            if (!context.Set<Genre>().Any())
            {
                context.Set<Genre>().AddOrUpdate(new Genre { Name = "Unknown Genre" });
                context.SaveChanges();
            }
        }
    }
}