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
            Bind<IDisposableRepository<Track>>().To<TrackRepository>();
            Bind<IDisposableRepository<Artist>>().To<Repository<Artist>>();
            Bind<IDisposableRepository<Album>>().To<AlbumRepositry>();
            Bind<IDisposableRepository<Feedback>>().To<FeedbackRepository>();
            Bind<IDisposableRepository<Vote>>().To<VoteRepository>();
            Bind<IDisposableRepository<Genre>>().To<Repository<Genre>>();
            Bind<IDisposableRepository<TrackPrice>>().To<TrackPriceRepository>();
            Bind<IDisposableRepository<AlbumPrice>>().To<AlbumPriceRepository>();
            Bind<IDisposableRepository<CurrencyRate>>().To<CurrencyRateRepository>();
            Bind<IDisposableRepository<Currency>>().To<Repository<Currency>>();
            Bind<IDisposableRepository<PriceLevel>>().To<Repository<PriceLevel>>();

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}