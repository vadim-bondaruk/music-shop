﻿namespace PVT.Q1._2017.Shop
{
    using System.Data.Entity.Core.Objects;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using FluentValidation.Mvc;
    using global::Shop.BLL;

    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;

    using PVT.Q1._2017.Shop.Areas.Management.Models;

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
            DefaultModelsMapper.MapModels();
        }
    }
}