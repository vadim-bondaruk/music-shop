namespace Shop.BLL.Utils
{
    using System;
    using System.Web.Security;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Shop.Infrastructure.Security;


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
        public void LogIn(string useridentity, string password, bool redirect = true)
        {
            if (useridentity == null)
            {
                throw new ArgumentException("useridentity");
            }

            if (password == null)
            {
                throw new ArgumentException("password");
            }

            User user;            

            using (var users = this._factory.GetUserRepository())
            {
                if (user != null)
                {
                    if (!user.Password.Equals(PasswordEncryptor.GetHashString(password), StringComparison.OrdinalIgnoreCase))
                    {
                        throw new UserValidationException("Не верный пароль", "Password");
                    }

                    if (redirect)
                    {
                        FormsAuthentication.RedirectFromLoginPage(useridentity, true);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(useridentity, true);
                    }
                }
                else
                {
                    throw new UserValidationException("Такой пользователь не зарегистрирован", "Useridentity");
                }           
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();            
        }
    }
}


    