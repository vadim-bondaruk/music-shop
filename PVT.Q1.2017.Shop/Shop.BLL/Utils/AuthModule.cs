namespace Shop.BLL.Utils
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Shop.Infrastructure.Security;        
    using Utils;

    /// <summary>
    /// Authentification module
    /// </summary>
    public class AuthModule : IAuthModule
    {
        /// <summary>
        /// Database or repository with users data
        /// </summary>
        protected readonly IRepositoryFactory Factory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public AuthModule(IRepositoryFactory factory)
        {
            this.Factory = factory;
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

            if (useridentity.Contains("@"))
            {
                user = this.Factory.GetUserRepository().GetAll(u => u.Email.Equals(useridentity, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            else
            {
                user = this.Factory.GetUserRepository().GetAll(u => u.Login.Equals(useridentity, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            
            if (user != null)
            {
                if (!user.Password.Equals(PasswordEncryptor.GetHashString(password), StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserValidationException("Pasword not confirm", "Password");
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
    }
}
