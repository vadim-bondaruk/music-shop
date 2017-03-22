namespace PVT.Q1._2017.Shop.App_Start
{
    using System;
    using System.Web;
    using Ninject;
    using Ninject.Web.Common;

    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel(new FluentValidationNinjectModule());
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            return kernel;
        }
    }
}