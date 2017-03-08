[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PVT.Q1._2017.Shop.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PVT.Q1._2017.Shop.App_Start.NinjectWebCommon), "Stop")]

namespace PVT.Q1._2017.Shop.App_Start
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    
    using global::Shop.BLL;

    /// <summary>
    /// 
    /// </summary>
    public static class NinjectWebCommon 
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(GetModules());
            DependencyResolver.SetResolver(new CustomDependencyResolver(kernel));
        }

        /// <summary>
        /// Retrieves All ninject Modules
        /// </summary>
        /// <returns>Returns Ninject Modules from BLL, DAL and Web</returns>
        private static INinjectModule[] GetModules()
        {
            return new INinjectModule[]
            {
                new DefaultServicesNinjectModule(),
                new WebNinjectModule()
            };
        }
    }
}
