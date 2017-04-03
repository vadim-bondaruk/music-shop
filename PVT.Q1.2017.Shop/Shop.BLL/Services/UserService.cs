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
            //return user != null || user.IsDeleted.Equals(false);
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

            if (IsUserExist(userIdentity))
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {
                    user = userIdentity.Contains("@") ? userRepository?.FirstOrDefault(u => u.Email == userIdentity)
                                                      : userRepository?.FirstOrDefault(u => u.Login == userIdentity);
                }
            }
            else
            {
                throw new UserValidationException("Вы ввели неправеьный ник или email", "");
            }

            return user.Id;
        }


        /// <summary>
        /// Updates user model data by Id
        /// </summary>
        /// <param name="user">model user</param>
        /// <param name="id">Id from model user</param>
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
        /// Returns the email by his login or password
        /// </summary>
        /// <param name="userIdentity">Login or password</param>
        /// <returns></returns>
        public string GetEmailByUserIdentity(string userIdentity)
        {
            if (IsUserExist(userIdentity))
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
                if(user.IsDeleted == true)
                {
                    throw new UserValidationException("Этот аккаунт был удалён", "");
                }
                return userEmail;
            }
            else
            {
                throw new UserValidationException("Вы ввели неправеьный ник или email", "");
            }
        }

        /// <summary>
        /// Update user password data by Id
        /// </summary>
        /// <param name="id">Id from model user</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        public bool UpdatePassword(int id, string newPassword)
        {
            var update = false;
            try
            {
                using (var userRepository = Factory.GetUserRepository())
                {
                    var userDB = userRepository.GetById(id);
                    userDB.Password = PasswordEncryptor.GetHashString(newPassword);                  
                    userRepository.AddOrUpdate(userDB);
                    userRepository.SaveChanges();
                }
                update = true;
            }
            catch (Exception ex)
            {
                // TODO: write data to log
                throw;
            }
            return update;
        }

        /// <summary>
        /// Update user password data by Id
        /// </summary>
        /// <param name="id">Id from model user</param>
        /// <param name="newPassword">new password</param>
        /// <param name="oldPassword">old password</param>
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
                // TODO: write data to log             
                throw;
            }
            return update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="newLogin"></param>
        /// <returns></returns>
        public bool UpdateLogin (string userIdentity, string newLogin)
        {
            if (!string.IsNullOrEmpty(userIdentity))
            {
                try
                {
                    using (var userRepository = this.Factory.GetUserRepository())
                    {
                        var user = userIdentity.Contains("@") ? userRepository?.FirstOrDefault(u => u.Email == userIdentity)
                                                          : userRepository?.FirstOrDefault(u => u.Login == userIdentity);
                        user.Login = newLogin;
                        userRepository.AddOrUpdate(user);
                        userRepository.SaveChanges();
                        return user != null;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: write data to log             
                    throw;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the user to the "Confirm Email" field in true
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool UpdateConfirmEmail (string token, string email)
        {
            try
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {                   
                    var user = userRepository.GetById(Int32.Parse(token));
                    if (user != null)
                    {
                        if (user.Email == email)
                        {
                            user.ConfirmedEmail = true;
                            userRepository.AddOrUpdate(user);
                            userRepository.SaveChanges();
                            return true;                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: write data to log             
                throw;
            }
            return false;
        }

        /// <summary>
        /// Changes the "IsDeleted" to "true"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete (int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("id");
            }
            try
            {
                using (var userRepository = Factory.GetUserRepository())
                {
                    var user = userRepository.GetById(id);
                    user.IsDeleted = true;
                    userRepository.AddOrUpdate(user);
                    userRepository.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: write data to log             
                throw;
            }           
        }

    }
}
