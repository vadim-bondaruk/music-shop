namespace Shop.DAL
{
    using System.Data.Entity;
    using Context;
    using Infrastruture;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Web.Common;
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
            // Bind<ShopContext>().ToSelf().InRequestScope();
            Bind<ITrackRepository>().To<TrackBaseRepository>();
            Bind<IArtistRepository>().To<ArtistBaseRepository>();
            Bind<IAlbumRepository>().To<AlbumBaseRepository>();
            Bind<IFeedbackRepository>().To<FeedbackBaseRepository>();
            Bind<IVoteRepository>().To<VoteBaseRepository>();
            Bind<IGenreRepository>().To<GenreBaseRepository>();
            Bind<ITrackPriceRepository>().To<TrackPriceBaseRepository>();
            Bind<IAlbumPriceRepository>().To<AlbumPriceBaseRepository>();
            Bind<ICurrencyRateRepository>().To<CurrencyRateBaseRepository>();
            Bind<ICurrencyRepository>().To<CurrencyBaseRepository>();
            Bind<IPriceLevelRepository>().To<PriceLevelBaseRepository>();
            Bind<IUserRepository>().To<UserBaseRepository>();

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}