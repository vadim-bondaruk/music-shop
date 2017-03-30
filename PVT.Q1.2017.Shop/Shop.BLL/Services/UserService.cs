namespace Shop.BLL.Services
{
    using System;
    using Common.Models;
    using DAL.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure.Enums;
    using Utils;
    using System.Linq;
    using Exceptions;

    /// <summary>
    /// The user service
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public UserService(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public bool IsUserExist(string userIdentity)
        {

                User user = null;

                if (!string.IsNullOrEmpty(userIdentity))
                {
                    using (var userRepository = this.Factory.GetUserRepository())
                    {
                        user = userIdentity.Contains("@") ? userRepository?.FirstOrDefault(u => u.Email == userIdentity)
                                                          : userRepository?.FirstOrDefault(u => u.Login == userIdentity);
                    } 
                }

                return user != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }


            user.Password = PasswordEncryptor.GetHashString(user.Password);
            user.UserRoles = this.GetDefaultUserRoles();

            try
            {
                using (var userRepository = Factory.GetUserRepository())
                {
                    userRepository.AddOrUpdate(user);
                    userRepository.SaveChanges();
                }

                using (var userDataRepository = Factory.GetUserDataRepository())
                {
                    var userData = new UserData
                    {
                        UserId = user.Id,
                        CurrencyId = ServiceHelper.GetDefaultCurrency(Factory).Id,
                        PriceLevelId = ServiceHelper.GetDefaultPriceLevel(Factory)
                    };

                    userDataRepository.AddOrUpdate(userData);
                    userDataRepository.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO: write data to log
                return false;
            }
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
        public int GetIdOflogin(string userIdentity)
        {
            User user = null;

            if (!string.IsNullOrEmpty(userIdentity))
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {
                    user = userIdentity.Contains("@") ? userRepository?.FirstOrDefault(u => u.Email == userIdentity)
                                                      : userRepository?.FirstOrDefault(u => u.Login == userIdentity);
                }
            }

            return user.Id;
        }


        /// <summary>
        /// Updates user model data by Id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePersonal(User user, int id)
        {
            var update = false;

            if (user == null)
            {
                throw new ArgumentException("user");
            }
            using (var userRepository = this.Factory.GetUserRepository())
            {
                var userUpdate = userRepository.GetById(id);
                userUpdate.FirstName = user.FirstName;
                userUpdate.LastName = user.LastName;
                userUpdate.Sex = user.Sex;
                userUpdate.BirthDate = user.BirthDate;
                userUpdate.Country = user.Country;
                userUpdate.PhoneNumber = user.PhoneNumber;

                userRepository.AddOrUpdate(userUpdate);
                userRepository.SaveChanges();

            }
                update = true;


            return update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity">Login or password</param>
        /// <returns></returns>
        public string GetEmailByUserIdentity(string userIdentity)
        {

            User user = null;
            string userEmail = string.Empty;
            int id = GetIdOflogin(userIdentity);
            using (var userRepository = this.Factory.GetUserRepository())
            {
                user = userRepository.GetById(id);
                if (user != null)
                {
                    userEmail = user.Email;
                }               
            }           
            return userEmail;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int id, string newPassword, string oldPassword)
        {
            var update = false;
            try
            {
                using (var userRepository = Factory.GetUserRepository())
                {
                    var userUpdate = userRepository.GetById(id);
                    if (userUpdate.Password.Equals(PasswordEncryptor.GetHashString(oldPassword)))
                    {
                        userUpdate.Password = PasswordEncryptor.GetHashString(newPassword);
                    }
                    else
                    {
                        throw new UserValidationException("Старый пароль введён не верно", "OldPassword");
                    }
                    userRepository.AddOrUpdate(userUpdate);
                    userRepository.SaveChanges();
                }
                update = true;
            }
            catch (Exception ex)
            {                
                throw;
            }
            return update;
        }

        
    }
}
