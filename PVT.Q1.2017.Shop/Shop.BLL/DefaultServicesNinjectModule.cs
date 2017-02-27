namespace Shop.BLL
{
    using Common.Models;
    using DAL;
    using Infrastructure.Services;
    using Infrastructure.Validators;
    using Ninject;
    using Ninject.Modules;
    using Services;
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

            Bind<IService<Track>>().To<Service<Track>>();
            Bind<IService<Album>>().To<Service<Album>>();
            Bind<IService<Artist>>().To<Service<Artist>>();
            Bind<IService<Feedback>>().To<Service<Feedback>>();
            Bind<IService<Vote>>().To<Service<Vote>>();
            Bind<IService<Genre>>().To<Service<Genre>>();
        }
    }
}