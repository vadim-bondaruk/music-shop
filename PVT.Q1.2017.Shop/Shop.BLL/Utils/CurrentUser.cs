namespace Shop.BLL.Utils
{
    using System;
    using System.Security.Principal;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class CurrentUser : IPrincipal
    {
        /// <summary>
        /// 
        /// </summary>
        private IIdentity _userIdentity;

        /// <summary>
        /// 
        /// </summary>
        private User _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public CurrentUser(User user)
        {
            this._user = user;
            this._userIdentity = new UserIdentity(this._user);
        }

        /// <summary>
        /// 
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this._userIdentity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get
            {
                return this._user.Id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (role == null)
            {
                throw new ArgumentException("role");
            }

            var userIdentity = this._userIdentity as UserIdentity;

            if (userIdentity == null)
            {
                return false;
            }

            if (this._user.UserRole.ToString().Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public bool IsInRole(UserRoles role)
        {
            return this._user.UserRole.Equals(role); 
        }
    }
}