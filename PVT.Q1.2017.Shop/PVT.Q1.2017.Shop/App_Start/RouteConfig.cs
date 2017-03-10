namespace PVT.Q1._2017.Shop
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Set default routes
        /// </summary>
        /// <param name="routes">Collection of routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "CreateUser",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Create", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
