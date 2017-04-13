namespace PVT.Q1._2017.Shop
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
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

            if (user == null || !user.Identity.IsAuthenticated || this._userRoles.Length == 0)
            {
                return false;
            }

            string userName = user.Identity.Name;

            // TODO: Get user from Repository by Name
            using (var repository = DependencyResolver.Current.GetService<IRepositoryFactory>().GetUserRepository())
            {
                var userDB = repository.FirstOrDefault(u => u.Login.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (!this._userRoles.ToList().Contains(userDB.UserRoles))
                {
                    return false;
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}