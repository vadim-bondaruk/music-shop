namespace Shop.BLL
{
    using Common.Models;
    using DAL;
    using Infrastructure.Validators;
    using Ninject;
    using Ninject.Modules;
    using Services;
    using Services.Infrastruture;
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

            Bind<IValidator<Artist>>().To<NamedEntityValidator<Artist>>();
            Bind<IValidator<Album>>().To<NamedEntityValidator<Album>>();
            Bind<IValidator<Track>>().To<TrackValidator>();

            Bind<ITrackService>().To<TrackService>();
            /*Bind<IService<Album>>().To<Service<Album>>();
            Bind<IService<Artist>>().To<Service<Artist>>();
            Bind<IService<Feedback>>().To<Service<Feedback>>();
            Bind<IService<Vote>>().To<Service<Vote>>();
            Bind<IService<Genre>>().To<Service<Genre>>();
            Bind<IService<TrackPrice>>().To<Service<TrackPrice>>();
            Bind<IService<AlbumPrice>>().To<Service<AlbumPrice>>();
            Bind<IService<Currency>>().To<Service<Currency>>();
            Bind<IService<CurrencyRate>>().To<Service<CurrencyRate>>();
            Bind<IService<PriceLevel>>().To<Service<PriceLevel>>();*/
        }
    }
}