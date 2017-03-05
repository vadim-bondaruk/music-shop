namespace Shop.BLL
{
    using Common.Models;
    using DAL.Context;
    using DAL.Repositories;
    using Ninject.Modules;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    /// 
    /// </summary>
    public class BllNinjectModule : NinjectModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            Bind<IRepository<User>>().To<UserRepository>();        
        }
    }
}