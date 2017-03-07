namespace Shop.DAL
{
    using System.Data.Entity;
    using Context;
    using Infrastruture;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Ninject.Web.Common;

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
            Bind<ShopContext>().ToSelf().InRequestScope();

            Bind<IRepositoryFactory>().ToFactory();
        }
    }
}