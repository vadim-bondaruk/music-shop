namespace Shop.BLL.Services
{
    using System;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Infrastructure;
    using Shop.Infrastructure.Enums;
    using Utils;
    using Validators;
    using PVT.Q1._2017.Shop.ViewModels;
    using Helpers;
    using System.Linq;

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

        /// <summary>
        /// Returns the Id by his login or password
        /// </summary>
        /// <param name="userIdentity">Login or password</param>
        /// <returns>Returns the Id </returns>
        public int GetIdOflogin (string userIdentity)
        {
            User user;   
            if (userIdentity.Contains("@"))
            {
                user = this._userRepossitory.GetAll(u => u.Email.Equals(userIdentity, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            else
            {
                user = this._userRepossitory.GetAll(u => u.Login.Equals(userIdentity, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            return user.Id;
        }


        /// <summary>
        /// Updates user model data by Id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool UpdatePersonal (User user, int Id)
        {
            var update = false;

            if (user == null)
            {
                throw new ArgumentException("user");
            }

            var userId = _userRepossitory.GetById(Id);
            userId.FirstName = user.FirstName;
            userId.LastName = user.LastName;
            userId.Sex = user.Sex;
            userId.BirthDate = user.BirthDate;
            userId.Country = user.Country;
            userId.PhoneNumber = user.PhoneNumber;

            try
            {
                using (var userRepository = this._userRepossitory)
                {
                    userRepository.AddOrUpdate(userId);
                    userRepository.SaveChanges();
                }

                update = true;
            }
            catch (Exception ex)
            {
                // write data to log
                throw;
            }

            return update;
        }

    }
}
