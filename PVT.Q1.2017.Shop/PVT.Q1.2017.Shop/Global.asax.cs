namespace PVT.Q1._2017.Shop
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Script.Serialization;
    using System.Web.Security;
    using BLL.Utils;
    using FluentValidation.Mvc;
    using global::Shop.Common.Models;

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

        /// <summary>
        /// Occurs when a security module has established the identity of the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserPrincipalSerializeModel serializeUser = serializer.Deserialize<UserPrincipalSerializeModel>(authTicket.UserData);

                if (serializeUser != null)
                {
                    User user = new User
                    {
                        Id = serializeUser.Id,
                        Login = serializeUser.Login,
                        Email = serializeUser.Email,
                        UserRoles = serializeUser.UserRole
                    };

                    CurrentUser newUser = new CurrentUser(user);

                    HttpContext.Current.User = newUser; 
                }
            }
        }
    }
}