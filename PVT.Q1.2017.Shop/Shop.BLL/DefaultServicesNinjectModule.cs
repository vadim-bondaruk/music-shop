namespace Shop.BLL
{
    using DAL;
    using Ninject;
    using Ninject.Modules;
    using Services;
    using Services.Infrastructure;
    using Utils;
    using Utils.Infrastructure;

    /// <summary>
    /// Default cofiguration module.
    /// </summary>
    public class DefaultServicesNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads configuration settings.
        /// </summary>
        public override void Load()
        {
            if (this.Kernel != null)
            {
                this.Kernel.Load(new DefaultRepositoriesNinjectModule());
            }

            this.BindServices();
        }
        
        /// <summary>
        /// Binds services.
        /// </summary>
        protected virtual void BindServices()
        {
            Bind<IArtistService>().To<ArtistService>();
            Bind<ITrackService>().To<TrackService>();
            Bind<IAlbumService>().To<AlbumService>();
            Bind<IGenreService>().To<GenreService>();

            Bind<IFeedbackService>().To<FeedbackService>();

            Bind<ITrackPriceService>().To<TrackPriceService>();
            Bind<IAlbumPriceService>().To<AlbumPriceService>();

            Bind<IUserPaymentMethodService>().To<UserPaymentMethodService>();
            Bind<IPaymentService>().To<PaymentService>();

            Bind<ICurrencyService>().To<CurrencyService>();

            Bind<ICurrencyRateService>().To<CurrencyRateService>();

            Bind<ICartService>().To<CartService>();
            Bind<IAuthModule>().To<AuthModule>();
            Bind<IUserService>().To<UserService>();
        }
    }
}