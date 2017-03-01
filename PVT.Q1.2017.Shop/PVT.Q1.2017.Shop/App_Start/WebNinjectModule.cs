namespace PVT.Q1._2017.Shop
{
    using Ninject.Modules;
    using global::Shop.BLL.Infrastructure;
    using global::Shop.BLL.Services;

    /// <summary>
    /// 
    /// </summary>
    public class WebNinjectModule : NinjectModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();                   
        }
    }
}