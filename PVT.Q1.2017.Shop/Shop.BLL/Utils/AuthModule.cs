namespace Shop.BLL.Utils
{
    using System;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Security;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Infrastructure;

    /// <summary>
    /// Authentification module
    /// </summary>
    public class AuthModule : IAuthModule
    {
        /// <summary>
        /// Database or repository with users data
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public AuthModule(IRepositoryFactory users)
        {
            this._factory = users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useridentity">User login or email</param>
        /// <param name="password"></param>
        /// <param name="redirect"></param>
        public void LogIn(string useridentity, string password, HttpContext context, bool isPersistent = true)
        {
            if (useridentity == null)
            {
                throw new ArgumentException("useridentity");
            }

            if (password == null)
            {
                throw new ArgumentException("password");
            }

            User user = this.GetUser(useridentity);

            if (user != null)
            {
                if (!user.Password.Equals(PasswordEncryptor.GetHashString(password), StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserValidationException("Pasword not confirm", "Password");
                }

                UserPrincipalSerializeModel userPrincipal = new UserPrincipalSerializeModel
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                    UserRole = user.UserRoles
                };

                context.Response.Cookies.Add(this.GetAuthCookies(userPrincipal, isPersistent));
            }
            else
            {
                throw new UserValidationException("User not found", "Useridentity");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Get HttpCookies for authentication
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <param name="expires"></param>
        private HttpCookie GetAuthCookies(UserPrincipalSerializeModel user, bool isPersistent = true, int expires = 1440)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string cookiesData = serializer.Serialize(user);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    user.Login,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(expires),
                    isPersistent,
                    cookiesData,
                    FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            var cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            if (ticket.IsPersistent)
            {
                cookies.Expires = ticket.Expiration;
            }
                
            return cookies;
        }

        /// <summary>
        /// Get user from repository
        /// </summary>
        /// <param name="useridentity"></param>
        /// <returns></returns>
        private User GetUser(string useridentity)
        {
            User user = null;

            if (!string.IsNullOrEmpty(useridentity))
            {
                using (var users = this._factory.GetUserRepository())
                {
                    if (useridentity.Contains("@"))
                    {
                        user = users.FirstOrDefault(u => u.Email.Equals(useridentity, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        user = users.FirstOrDefault(u => u.Login.Equals(useridentity, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }

            return user;
        }
    }
}
