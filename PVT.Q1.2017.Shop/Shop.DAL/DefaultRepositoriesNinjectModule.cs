namespace Shop.DAL
{
    #region using

    using System.Data.Entity;
    using Ninject;
    using Ninject.Modules;

    using Context;

    using Ninject.Extensions.Factory;

    using Shop.DAL.Repositories;
    using Shop.DAL.Repositories.Infrastruture;
    using Shop.Infrastructure;

    #endregion

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
            this.Bind<ITrackRepository>().To<TrackRepository>();
            this.Bind<IArtistRepository>().To<ArtistRepository>();
            this.Bind<IAlbumRepository>().To<AlbumRepository>();
            this.Bind<IFeedbackRepository>().To<FeedbackRepository>();
            this.Bind<IVoteRepository>().To<VoteRepository>();
            this.Bind<IGenreRepository>().To<GenreRepository>();
            this.Bind<ITrackPriceRepository>().To<TrackPriceRepository>();
            this.Bind<IAlbumPriceRepository>().To<AlbumPriceRepository>();
            this.Bind<ICurrencyRateRepository>().To<CurrencyRateRepository>();
            this.Bind<ICurrencyRepository>().To<CurrencyRepository>();
            this.Bind<IPriceLevelRepository>().To<PriceLevelRepository>();
            this.Bind<IUserRepository>().To<UserRepository>();

            // Bind<IControllerFactory>().To<ControllerFactory>();
            this.Bind<IFactory>().ToFactory();
        }
    }
}