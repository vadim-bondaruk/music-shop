namespace Shop.DAL
{
    using System.Data.Entity;
    using Common.Models;
    using Context;
    using Infrastructure.Repositories;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Repositories;

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
            Bind<DbContext>().To<ShopContext>();

            Bind<IRepository<Track>>().To<Repository<Track>>();
            Bind<IRepository<Artist>>().To<Repository<Artist>>();
            Bind<IRepository<Album>>().To<Repository<Album>>();
            Bind<IRepository<Feedback>>().To<Repository<Feedback>>();
            Bind<IRepository<Vote>>().To<Repository<Vote>>();
            Bind<IRepository<Genre>>().To<Repository<Genre>>();

            this.ConfigureRepositoryFactory();
        }

        /// <summary>
        /// Configures the repository factory.
        /// </summary>
        protected virtual void ConfigureRepositoryFactory()
        {
            Bind<IDisposableRepository<Track>>().To<Repository<Track>>();
            Bind<IDisposableRepository<Artist>>().To<Repository<Artist>>();
            Bind<IDisposableRepository<Album>>().To<Repository<Album>>();
            Bind<IDisposableRepository<Feedback>>().To<Repository<Feedback>>();
            Bind<IDisposableRepository<Vote>>().To<Repository<Vote>>();
            Bind<IDisposableRepository<Genre>>().To<Repository<Genre>>();

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}