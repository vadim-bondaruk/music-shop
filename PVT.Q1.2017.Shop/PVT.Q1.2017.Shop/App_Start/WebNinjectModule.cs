// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebNinjectModule.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines bindings for application
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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