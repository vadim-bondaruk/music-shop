namespace PVT.Q1._2017.Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    /// <summary>
    /// Web api config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register configs
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                });
        }
    }
}
