namespace Shop.BLL
{
    using Common.Models;
    using DAL;
    using Infrastructure.Validators;
    using Ninject;
    using Ninject.Modules;
    using Services;
    using Services.Infrastructure;
    using Validators;

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

            this.BindValidators();
            this.BindServices();
        }

        /// <summary>
        /// Binds validators.
        /// </summary>
        protected virtual void BindValidators()
        {
            Bind<IValidator<Artist>>().To<NamedEntityValidator<Artist>>();
            Bind<IValidator<Album>>().To<AlbumValidator>();
            Bind<IValidator<Track>>().To<TrackValidator>();

            Bind<IValidator<Genre>>().To<NamedEntityValidator<Genre>>();
            Bind<IValidator<Feedback>>().To<FeedbackValidator>();
            Bind<IValidator<Vote>>().To<VoteValidator>();

            Bind<IValidator<PriceLevel>>().To<NamedEntityValidator<PriceLevel>>();
            Bind<IValidator<TrackPrice>>().To<TrackPriceValidator>();
            Bind<IValidator<AlbumPrice>>().To<AlbumPriceValidator>();
            Bind<IValidator<Currency>>().To<CurrencyValidator>();
        }

        /// <summary>
        /// Binds services.
        /// </summary>
        protected virtual void BindServices()
        {
            Bind<ITrackService>().To<TrackService>();
            Bind<IArtistService>().To<ArtistService>();
            Bind<IAlbumService>().To<AlbumService>();

            Bind<IGenreService>().To<GenreService>();
            Bind<IVoteService>().To<VoteService>();
            Bind<IFeedbackService>().To<FeedbackService>();

            Bind<IPriceLevelService>().To<PriceLevelService>();
            Bind<ITrackPriceService>().To<TrackPriceService>();
            Bind<IAlbumPriceService>().To<AlbumPriceService>();

            Bind<IUserService>().To<UserService>();

            Bind<ICurrencyService>().To<CurrencyService>();
            /*Bind<ICurrencyRateService>().To<CurrencyRateService>();*/
        }
    }
}