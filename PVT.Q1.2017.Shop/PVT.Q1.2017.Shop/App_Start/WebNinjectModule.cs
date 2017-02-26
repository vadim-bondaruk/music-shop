namespace PVT.Q1._2017.Shop
{
    #region

    using System.Threading;

    using Ninject.Modules;

    #endregion

    /// <summary>
    ///     Defines bindings for application
    /// </summary>
    public class WebNinjectModule : NinjectModule
    {
        /// <summary>
        ///     Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Thread.Sleep(1);
        }
    }
}