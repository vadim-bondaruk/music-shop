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
            Bind<ITrackRepository>().To<TrackBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackRepository());
            Bind<IArtistRepository>().To<ArtistBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetArtistRepository());
            Bind<IAlbumRepository>().To<AlbumBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumRepository());
            Bind<IFeedbackRepository>().To<FeedbackBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetFeedbackRepository());
            Bind<IVoteRepository>().To<VoteBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetVoteRepository());
            Bind<IGenreRepository>().To<GenreBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetGenreRepository());
            Bind<ITrackPriceRepository>().To<TrackPriceBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackPriceRepository());
            Bind<IAlbumPriceRepository>().To<AlbumPriceBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumPriceRepository());
            Bind<ICurrencyRateRepository>().To<CurrencyRateBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRateRepository());
            Bind<ICurrencyRepository>().To<CurrencyBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRepository());
            Bind<IPriceLevelRepository>().To<PriceLevelBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetPriceLevelRepository());
            Bind<IUserRepository>().To<UserBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetUserRepository());
            Bind<ICartRepository>().To<CartBaseRepository>().NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCartRepository());

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}