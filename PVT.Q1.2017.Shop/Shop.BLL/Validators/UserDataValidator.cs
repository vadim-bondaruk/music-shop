namespace Shop.BLL.Validators
{
    using System;
    using System.Linq;
    using Ship.Infrastructure.Repositories;
    using Shop.Common.Models;
    using Shop.Common.Utils;

    /// <summary>
    /// 
    /// </summary>
    public static class UserDataValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static bool IsLoginUnique(string login, IRepository<User> repository)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException("login");
            }

            if (repository.GetAll().Where(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)).IsAny<User>())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmailUnique(string email, IRepository<User> repository)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email");
            }

            if (repository.GetAll().Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).IsAny<User>())
            {
                return false;
            }

            return true;
        }
    }
}
