namespace Shop.BLL.Utils
{
    using System;
    using System.Web.Security;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using System.Web;
    using Infrastructure;
    using PVT.Q1._2017.BLL.Utils;
    using System.Security.Principal;
    using System.Web.Script.Serialization;

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
        /// Initializes a new instance of the <see cref="AuthModule"/> class.
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
        public void LogIn(string useridentity, string password, HttpContext context, bool redirec)
        {
            if (useridentity == null)
            {
                throw new ArgumentException("useridentity");
            }

            if (password == null)
            {
                throw new ArgumentException("password");
            }

            User user = GetUser(useridentity);            
            
                if (user != null)
                {
                    if (!user.Password.Equals(PasswordEncryptor.GetHashString(password), StringComparison.OrdinalIgnoreCase))
                    {
                        throw new UserValidationException("Не верный пароль", "Password");
                    }

                    UserPrincipalSerializeModel userPrincipal = new UserPrincipalSerializeModel
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Email = user.Email
                    };
                 
                    if (!user.ConfirmedEmail)
                    {
                        throw new UserValidationException("Не подтвержден email", "");
                    }

                    context.Response.Cookies.Add(GetAuthCookies(userPrincipal, redirec));                

                    //if (redirect)
                    //{
                    //    FormsAuthentication.RedirectFromLoginPage(useridentity, true);
                    //}
                    //else
                    //{
                    //    FormsAuthentication.SetAuthCookie(useridentity, true);
                    //}
            }
                else
                {
                    throw new UserValidationException("Такой пользователь не зарегистрирован", "Useridentity");
                }
                    //return new CurrentUser(user);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <param name="expires"></param>
        public HttpCookie GetAuthCookies(UserPrincipalSerializeModel user, bool isPersistent, int expires = 20)
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
                    cookiesData);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            return new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);       
        }

        /// <summary>
        /// 
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
