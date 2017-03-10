namespace PVT.Q1._2017.Shop
{
    #region

    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;
    using FluentValidation.Mvc;

    using global::Shop.Common.Models;
    using ViewModels;

    #endregion

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

            FluentValidationModelValidatorProvider.Configure();
            //// Mapper.Initialize(cfg => cfg.CreateMap<Track, TrackViewModel>());
        }
    }
}