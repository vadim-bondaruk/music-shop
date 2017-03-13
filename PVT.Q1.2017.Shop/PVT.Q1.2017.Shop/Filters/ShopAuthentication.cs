namespace PVT.Q1._2017.Shop.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using System.Web.Routing;
    using global::Shop.DAL.Context;

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ShopAuthentication : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            IIdentity ident = filterContext.Principal.Identity;
            if (!ident.IsAuthenticated || !ident.Name.EndsWith("@test@tut.by"))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {
                            "controller",
                            "User"
                        },
                        {
                            "action",
                            "Login"
                        },
                        {
                            "returnUrl", filterContext.HttpContext.Request.RawUrl
                        }
                });
            }
        }
    }
}