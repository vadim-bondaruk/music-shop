namespace PVT.Q1._2017.Shop
{
    #region

    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using global::Shop.BLL;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

    using Ninject;

    using PVT.Q1._2017.Shop.Views.ViewModels;

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

            var kernel = new StandardKernel(new DefaultServicesNinjectModule());
            var repositoryFactory = kernel.Get<IRepositoryFactory>();
            var repo = repositoryFactory.CreateRepository<Track>();

            Mapper.Initialize(cfg => cfg.CreateMap<Track, TrackViewModel>());
            Mapper.Map<IEnumerable<Track>, List<TrackViewModel>>(repo.GetAll());
        }
    }
}