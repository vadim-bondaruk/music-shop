namespace PVT.Q1._2017.Shop
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Management.Extensions;
    using Areas.Management.ViewModels;
    using Antlr.Runtime.Misc;
    using App_Start;
    using AutoMapper;
    using FluentValidation.Mvc;
    using Ninject;
    using Ninject.Web.Common;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.BLL;

    /// <summary>
    ///     Base class in an ASP.NET application
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        ///     Start application
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FluentValidationModelValidatorProvider
                .Configure(provider => provider.ValidatorFactory = new CustomValidatorFactory(FluentValidationHelper.GetKernel()));
        }
    }
}