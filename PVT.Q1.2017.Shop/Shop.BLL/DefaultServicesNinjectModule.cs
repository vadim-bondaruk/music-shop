namespace Shop.BLL
{
    using Common.Models;
    using DAL;
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

            Bind<ITrackService>().To<TrackService>();
            Bind<IValidator<Artist>>().To<ArtistValidator>();
            Bind<IValidator<Album>>().To<AlbumValidator>();
            Bind<IValidator<Track>>().To<TrackValidator>();
        }
    }
}