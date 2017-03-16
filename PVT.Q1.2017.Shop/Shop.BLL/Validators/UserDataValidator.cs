﻿namespace Shop.BLL.Validators
{
    using System;
    using System.Linq;
    using Shop.Common.Models;
    using Shop.Common.Utils;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    /// 
    /// </summary>
    public static class UserDataValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static bool IsLoginUnique(string login, IRepository<User> repository)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException("login");
            }

            if (repository.GetAll().Where(u => u.Login != null && u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)).IsAny<User>())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static bool IsEmailUnique(string email, IRepository<User> repository)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email");
            }

            if (repository.GetAll().Where(u => u.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).IsAny<User>())
            {
                return false;
            }

            return true;
        }
    }
}
