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
    ///     The default repositories bindings configuration
    /// </summary>
    public class DefaultRepositoriesNinjectModule : NinjectModule
    {
        /// <summary>
        ///     Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<DbContext>().To<ShopContext>().InRequestScope();
            this.ConfigureRepositoryFactory();
        }

        /// <summary>
        ///     Configures the repository factory.
        /// </summary>
        protected virtual void ConfigureRepositoryFactory()
        {
            // Bind<ShopContext>().ToSelf().InRequestScope();
            this.Bind<ITrackRepository>()
                .To<TrackRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackRepository());
            this.Bind<IArtistRepository>()
                .To<ArtistRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetArtistRepository());
            this.Bind<IAlbumRepository>()
                .To<AlbumRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumRepository());
            this.Bind<IFeedbackRepository>()
                .To<FeedbackRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetFeedbackRepository());
            this.Bind<IVoteRepository>()
                .To<VoteRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetVoteRepository());
            this.Bind<IGenreRepository>()
                .To<GenreRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetGenreRepository());
            this.Bind<ITrackPriceRepository>()
                .To<TrackPriceRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetTrackPriceRepository());
            this.Bind<IAlbumPriceRepository>()
                .To<AlbumPriceRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumPriceRepository());
            this.Bind<ICurrencyRateRepository>()
                .To<CurrencyRateRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRateRepository());
            this.Bind<ICurrencyRepository>()
                .To<CurrencyRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetCurrencyRepository());
            this.Bind<IPriceLevelRepository>()
                .To<PriceLevelRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetPriceLevelRepository());
            this.Bind<IUserDataRepository>()
                .To<UserDataRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetUserDataRepository());
            this.Bind<IAlbumTrackRelationRepository>()
                .To<AlbumTrackRelationRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetAlbumTrackRelationRepository());
            this.Bind<IUserPaymentMethodRepository>()
                .To<UserPaymentMethodRepository>()
                .NamedLikeFactoryMethod((IRepositoryFactory f) => f.GetUserPaymentMethodRepository());

            this.Bind<IRepositoryFactory>().ToFactory();
        }
    }
}