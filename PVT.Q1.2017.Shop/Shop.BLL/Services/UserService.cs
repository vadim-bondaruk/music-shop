﻿namespace Shop.BLL.Services
{    
    using System;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Infrastructure;
    using Shop.Infrastructure.Enums;        
    using Utils;
    using Validators;

    /// <summary>
    /// The user service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserRepository _userRepossitory;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository) 
        {
            this._userRepossitory = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(User user)
        {
            var registered = false;

            if (user == null)
            {
                throw new ArgumentException("user");
            }

            // if (!UserDataValidator.IsLoginUnique(user.Login, this._userRepossitory))
            // {
            //     throw new UserValidationException("User with the same login already exists", "Login");
            // }
               
            // if (!UserDataValidator.IsEmailUnique(user.Email, this._userRepossitory))
            // {
            //     throw new UserValidationException("User with the same email already exists", "Email");
            // }
            user.Password = PasswordEncryptor.GetHashString(user.Password);
            user.UserRoles = this.GetDefaultUserRoles();
           
            try
            {
                using (var userRepository = this._userRepossitory)
                {
                    userRepository.AddOrUpdate(user);
                    userRepository.SaveChanges();
                }

                registered = true;
            }
            catch (Exception)
            {
                // write data to log
                throw;
            }

            return registered;
        }            

        /// <summary>
        /// Getting default user role
        /// </summary>
        /// <returns></returns>
        private UserRoles GetDefaultUserRoles()
        {
            return UserRoles.User;
        }
    }
}
