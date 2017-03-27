﻿namespace PVT.Q1._2017.Shop.App_Start
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PVT.Q1._2017.Shop.Controllers;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Enums;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ShopAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        private UserRoles[] _userRoles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="UserRoles"> Array UserRoles enum </param>
        public ShopAuthorizeAttribute(params UserRoles[] userRoles)
        {
            this._userRoles = userRoles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentException("httpContext");
            }
            
            var user = httpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            string userName = user.Identity.Name;

            // TODO: Get user from Repository by Name
            var userDB = DependencyResolver.Current.GetService<IRepositoryFactory>()
                .GetUserRepository()?.FirstOrDefault(u =>u.Login.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null && this._userRoles.Length > 0)
            {
                if (!this._userRoles.ToList().Contains(userDB.UserRoles))
                {
                    return false;
                }
                else
                {
                    httpContext.User = new CurrentUser(userDB);
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}