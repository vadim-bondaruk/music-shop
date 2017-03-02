namespace Shop.DAL
{
    using System.Data.Entity;
    using Context;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using Infrastructure.Repositories;
    using Infrastructure.Core;
    using Infrastructure.Models;

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
            Bind(typeof(IRepository<>), typeof(Repository<>));
            Bind<IFactory>().ToFactory();
        }
    }
}