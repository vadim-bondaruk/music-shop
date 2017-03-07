namespace Shop.DAL
{
    using System.Data.Entity;
    using Context;
    using Infrastructure;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Repositories;
    using Repositories.Infrastruture;

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
            Bind<ITrackRepository>().To<TrackRepository>();
            Bind<IArtistRepository>().To<ArtistRepository>();
            Bind<IAlbumRepository>().To<AlbumRepository>();
            Bind<IFeedbackRepository>().To<FeedbackRepository>();
            Bind<IVoteRepository>().To<VoteRepository>();
            Bind<IGenreRepository>().To<GenreRepository>();
            Bind<ITrackPriceRepository>().To<TrackPriceRepository>();
            Bind<IAlbumPriceRepository>().To<AlbumPriceRepository>();
            Bind<ICurrencyRateRepository>().To<CurrencyRateRepository>();
            Bind<ICurrencyRepository>().To<CurrencyRepository>();
            Bind<IPriceLevelRepository>().To<PriceLevelRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICartRepository>().To<CartRepository>();

            Bind<IFactory>().ToFactory();
        }
    }
}