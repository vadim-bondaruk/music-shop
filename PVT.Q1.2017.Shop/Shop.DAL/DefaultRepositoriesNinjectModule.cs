namespace Shop.DAL
{
    using System.Data.Entity;
    using Context;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Infrastructure.Repositories;
    using Infrastructure.Core;

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

            this.ConfigureRepositoryFactory();
        }

        /// <summary>
        /// Configures the repository factory.
        /// </summary>
        protected virtual void ConfigureRepositoryFactory()
        {
            Bind<IRepository>().To<ShopContext>();
            Bind<IFactory>().ToFactory();
        }
    }
}