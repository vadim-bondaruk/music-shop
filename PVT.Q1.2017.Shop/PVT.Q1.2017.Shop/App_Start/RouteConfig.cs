// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Global filters of route table
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop
{
    #region

    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    /// <summary>
    ///     Global filters of route table
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Set default <paramref name="routes" />
        /// </summary>
        /// <param name="routes">Collection of routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}