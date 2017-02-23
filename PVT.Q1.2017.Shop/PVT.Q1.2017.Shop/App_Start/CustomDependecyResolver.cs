namespace PVT.Q1._2017.Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Ninject;

    /// <summary>
    /// qq
    /// </summary>
    public class CustomDependecyResolver : IDependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        public CustomDependecyResolver(IKernel kernel)
        {
            this._kernel = kernel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return this._kernel.Get(serviceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType);
        }
    }
}