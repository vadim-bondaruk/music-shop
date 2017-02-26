namespace Shop.BLL
{
    using DAL;
    using Ninject;
    using Ninject.Modules;
    using Services;

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
        }
    }
}