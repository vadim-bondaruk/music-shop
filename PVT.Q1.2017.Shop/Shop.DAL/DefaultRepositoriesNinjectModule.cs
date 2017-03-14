namespace Shop.DAL
{
    using System.Data.Entity;

    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Web.Common;

    using Shop.DAL.Context;
    using Shop.DAL.Infrastruture;
    using Shop.DAL.Repositories;

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
            this.Bind<DbContext>().To<ShopContext>().InRequestScope();
            this.ConfigureRepositoryFactory();
        }

        /// <summary>
        /// Configures the repository factory.
        /// </summary>
        protected virtual void ConfigureRepositoryFactory()
        {
            // Bind<ShopContext>().ToSelf().InRequestScope();
            this.Bind<ITrackRepository>()
                .To<TrackBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackRepository());
            this.Bind<IArtistRepository>()
                .To<ArtistBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetArtistRepository());
            this.Bind<IAlbumRepository>()
                .To<AlbumBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumRepository());
            this.Bind<IFeedbackRepository>()
                .To<FeedbackBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetFeedbackRepository());
            this.Bind<IVoteRepository>()
                .To<VoteBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetVoteRepository());
            this.Bind<IGenreRepository>()
                .To<GenreBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetGenreRepository());
            this.Bind<ITrackPriceRepository>()
                .To<TrackPriceBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackPriceRepository());
            this.Bind<IAlbumPriceRepository>()
                .To<AlbumPriceBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumPriceRepository());
            this.Bind<ICurrencyRateRepository>()
                .To<CurrencyRateBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRateRepository());
            this.Bind<ICurrencyRepository>()
                .To<CurrencyBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRepository());
            this.Bind<IPriceLevelRepository>()
                .To<PriceLevelBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetPriceLevelRepository());
            this.Bind<IUserRepository>()
                .To<UserBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetUserRepository());
            this.Bind<IAlbumTrackRelationRepository>()
                .To<AlbumTrackRelationBaseRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumTrackRelationRepository());

            this.Bind<IUserPaymentMethodRepository>()
                .To<UserPaymentMethodRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetUserPaymentMethodRepository());

            this.Bind<IRepositoryFactory>().ToFactory();
        }
    }
}