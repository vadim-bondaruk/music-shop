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
            Bind<IValidator<Album>>().To<NamedEntityValidator<Album>>();
            Bind<IValidator<Track>>().To<TrackValidator>();
            Bind<IValidator<Feedback>>().To<FeedbackValidator>();
            Bind<IValidator<Vote>>().To<VoteValidator>();
            Bind<IValidator<PriceLevel>>().To<NamedEntityValidator<PriceLevel>>();
        }

        /// <summary>
        /// Binds services.
        /// </summary>
        protected virtual void BindServices()
        {
            Bind<ITrackService>().To<TrackService>();

            Bind<IVoteService>().To<VoteService>();
            Bind<IFeedbackService>().To<FeedbackService>();
            Bind<IPriceLevelService>().To<PriceLevelService>();
            /*Bind<IAlbumService>().To<AlbumService>();
            Bind<IArtistService>().To<ArtistService>();
            /*Bind<IService<Genre>>().To<Service<Genre>>();
            Bind<IService<TrackPrice>>().To<Service<TrackPrice>>();
            Bind<IService<AlbumPrice>>().To<Service<AlbumPrice>>();
            Bind<IService<Currency>>().To<Service<Currency>>();
            Bind<IService<CurrencyRate>>().To<Service<CurrencyRate>>();*/
        }
    }
}