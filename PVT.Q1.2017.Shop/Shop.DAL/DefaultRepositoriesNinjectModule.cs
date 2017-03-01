namespace Shop.DAL
{
    using System.Data.Entity;
    using Common.Models;
    using Context;
    using Infrastructure.Repositories;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Repositories;

    /// <summary>
    /// The default repositories bindings configuration
    /// </summary>
    public class DefaultRepositoriesNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<DbContext>().To<ShopContext>();

            this.ConfigureRepositoryFactory();
        }

        /// <summary>
        /// Configures the repository factory.
        /// </summary>
        protected virtual void ConfigureRepositoryFactory()
        {
            Bind<IRepository<Track>>().To<TrackRepository>();
            Bind<IRepository<Artist>>().To<Repository<Artist>>();
            Bind<IRepository<Album>>().To<AlbumRepositry>();
            Bind<IRepository<Feedback>>().To<FeedbackRepository>();
            Bind<IRepository<Vote>>().To<VoteRepository>();
            Bind<IRepository<Genre>>().To<Repository<Genre>>();
            Bind<IRepository<TrackPrice>>().To<TrackPriceRepository>();
            Bind<IRepository<AlbumPrice>>().To<AlbumPriceRepository>();
            Bind<IRepository<CurrencyRate>>().To<CurrencyRateRepository>();
            Bind<IRepository<Currency>>().To<Repository<Currency>>();
            Bind<IRepository<PriceLevel>>().To<Repository<PriceLevel>>();

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}