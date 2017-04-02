namespace PVT.Q1._2017.Shop
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Management.Extensions;
    using Areas.Management.ViewModels;
    using AutoMapper;
    using FluentValidation.Mvc;
    using global::Shop.Common.Models;
    using System;
    using System.Web.Security;
    using System.Web.Script.Serialization;
    using BLL.Utils;

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

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserPrincipalSerializeModel serializeUser = serializer.Deserialize<UserPrincipalSerializeModel>(authTicket.UserData);

                User user = new User
                {
                    Id = serializeUser.Id,
                    Login = serializeUser.Login,
                    Email = serializeUser.Email
                };
                CurrentUser newUser = new CurrentUser(user);


                HttpContext.Current.User = newUser;
            }
        }
    }
}