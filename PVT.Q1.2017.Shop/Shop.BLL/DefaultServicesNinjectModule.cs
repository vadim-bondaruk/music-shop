namespace Shop.BLL
{
    using Ninject;
    using Ninject.Modules;

    using Shop.BLL.Services;
    using Shop.BLL.Services.Infrastructure;
    using Shop.BLL.Utils;
    using Shop.BLL.Utils.Infrastructure;
    using Shop.DAL;

    /// <summary>
    ///     Default cofiguration module.
    /// </summary>
    public class DefaultServicesNinjectModule : NinjectModule
    {
        /// <summary>
        ///     Loads configuration settings.
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
        ///     Binds services.
        /// </summary>
        protected virtual void BindServices()
        {
            this.Bind<IArtistService>().To<ArtistService>();
            this.Bind<ITrackService>().To<TrackService>();
            this.Bind<IAlbumService>().To<AlbumService>();
            this.Bind<IGenreService>().To<GenreService>();
            this.Bind<IFeedbackService>().To<FeedbackService>();
            this.Bind<ITrackPriceService>().To<TrackPriceService>();
            this.Bind<IAlbumPriceService>().To<AlbumPriceService>();
            this.Bind<IUserPaymentMethodService>().To<UserPaymentMethodService>();
            this.Bind<IPaymentService>().To<PaymentService>();
            this.Bind<ICurrencyService>().To<CurrencyService>();
            this.Bind<ICurrencyRateService>().To<CurrencyRateService>();
            this.Bind<ICartService>().To<CartService>();
            this.Bind<IAuthModule>().To<AuthModule>();
            this.Bind<IUserService>().To<UserService>();
            this.Bind<ISettingService>().To<SettingService>();
        }
    }
}