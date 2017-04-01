namespace PVT.Q1._2017.Shop
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using FluentValidation.Mvc;

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
        }
    }
}