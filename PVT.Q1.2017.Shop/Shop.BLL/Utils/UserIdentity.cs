namespace Shop.BLL.Utils
{
    using System.Security.Principal;
    using global::Shop.Common.Models;

    /// <summary>
    /// 
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        private User _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public UserIdentity(User user)
        {
            this._user = user;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return this._user != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return this._user.Login;
            }
        }
    }
}