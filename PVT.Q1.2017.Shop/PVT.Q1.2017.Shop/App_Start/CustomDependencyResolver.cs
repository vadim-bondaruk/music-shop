﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomDependencyResolver.cs" company="">
//   
// </copyright>
// <summary>
//   Custom Dependency Resolver class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using Ninject;

    using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

    #endregion

    /// <summary>
    ///     Custom Dependency Resolver class
    /// </summary>
    public class CustomDependencyResolver : IDependencyResolver
    {
        /// <summary>
        ///     Application kernel of DependencyResolver
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDependencyResolver"/> class. 
        /// Initializes a new instance
        /// </summary>
        /// <param name="kernel">
        /// Kernel
        /// </param>
        public CustomDependencyResolver(IKernel kernel)
        {
            this._kernel = kernel;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// *****
        /// </exception>
        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// *****
        /// </exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object
        ///     creation
        /// </summary>
        /// <param name="serviceType">
        /// The type of the requested service or object
        /// </param>
        /// <returns>
        /// The requested service or object
        /// </returns>
        public object GetService(Type serviceType)
        {
            return this._kernel.Get(serviceType);
        }

        /// <summary>
        /// Resolves multiply registered services
        /// </summary>
        /// <param name="serviceType">
        /// The type of the requested services
        /// </param>
        /// <returns>
        /// The requested services
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType);
        }
    }
}