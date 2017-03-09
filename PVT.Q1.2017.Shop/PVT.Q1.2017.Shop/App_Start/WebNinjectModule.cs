namespace PVT.Q1._2017.Shop
{
    using Ninject.Modules;

    /// <summary>
    /// Defines bindings for application
    /// </summary>
    /// 
    /// </summary>
    public class WebNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();                   
        }
    }
}