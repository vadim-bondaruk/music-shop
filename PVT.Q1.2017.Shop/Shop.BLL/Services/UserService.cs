namespace Shop.BLL.Services
{
    using System;
    using Common.Models;
    using DAL.Infrastruture;
    using Exceptions;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure.Enums;
    using Utils;
    using Common.Validators.Infrastructure;

    /// <summary>
    /// The user service
    /// </summary>
    public class UserService : BaseService, IUserService, IUserValidator
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
            User user = this.GetUserByUserIdentity(userIdentity);

            //return user != null || user.IsDeleted.Equals(false);
            return user != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsUserExist(string userIdentity, out User user)
        {
            user = this.GetUserByUserIdentity(userIdentity);

            return user != null && user.IsDeleted.Equals(false);
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
            user.UserRole = this.GetDefaultUserRoles();

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
        /// Returns the Id by his login or password
        /// </summary>
        /// <param name="userIdentity">Login or password</param>
        /// <returns>Returns the Id </returns>
        public int GetIdOflogin(string userIdentity)
        {
            User user = null;

            if (this.IsUserExist(userIdentity, out user))
            {
                return user.Id;
            }
            else
            {
                throw new UserValidationException("Вы ввели неправильный ник или email", string.Empty);
            }
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

                try
                {
                    userRepository.AddOrUpdate(userUpdate);
                    userRepository.SaveChanges();
                    update = true;
                }
                catch (Exception ex)
                {
                    //TODO: Write Data to log
                    throw;
                }
            }

            return update;
        }

        /// <summary>
        /// Returns the email by his login or password
        /// </summary>
        /// <param name="userIdentity">Login or password</param>
        /// <returns></returns>
        public string GetEmailByUserIdentity(string userIdentity)
        {
            User user = null;

            if (this.IsUserExist(userIdentity, out user))
            {
                string userEmail = user.Email;

                if (user.IsDeleted == true)
                {
                    throw new UserValidationException("Этот аккаунт был удалён", string.Empty);
                }

                return userEmail;
            }
            else
            {
                throw new UserValidationException("Вы ввели неправеьный ник или email", string.Empty);
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
            if (string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

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
            if(string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(oldPassword))
            {
                return false;
            }

            var update = false;

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

                try
                {
                    userRepository.AddOrUpdate(userUpdate);
                    userRepository.SaveChanges();
                    update = true;
                }
                catch (Exception ex)
                {
                    //TODO: Write data to log
                    throw;
                }
            }

            return update;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="newLogin"></param>
        /// <returns></returns>
        public bool UpdateLogin(string userIdentity, string newLogin)
        {
            if (!string.IsNullOrEmpty(userIdentity) && !string.IsNullOrEmpty(newLogin))
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {
                    var user = userIdentity.Contains("@") ? userRepository?.FirstOrDefault(u => u.Email == userIdentity)
                                                      : userRepository?.FirstOrDefault(u => u.Login == userIdentity);
                    if (user != null)
                    {
                        try
                        {
                            user.Login = newLogin;
                            userRepository.AddOrUpdate(user);
                            userRepository.SaveChanges();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            //TODO: write dala to log
                            throw;
                        }
                    }
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
        public bool UpdateConfirmEmail(string token, string email)
        {
            using (var userRepository = this.Factory.GetUserRepository())
            {
                var user = userRepository.GetById(Int32.Parse(token));
                if (user != null)
                {
                    if (user.Email == email)
                    {
                        user.ConfirmedEmail = true;
                        try
                        {
                            userRepository.AddOrUpdate(user);
                            userRepository.SaveChanges();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            //TODO: write data to log
                            throw;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Changes the "IsDeleted" to "true"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete(int id)
        {
            bool deleted = false;

            if (id == 0)
            {
                throw new ArgumentException("id");
            }

            using (var userRepository = Factory.GetUserRepository())
            {
                var user = userRepository.GetById(id);
                if (user != null)
                {
                    user.IsDeleted = true;
                    try
                    {
                        userRepository.AddOrUpdate(user);
                        userRepository.SaveChanges();
                        deleted = true;
                    }
                    catch (Exception ex)
                    {
                        //TODO: write data to log
                        throw;
                    }
                }
            }

            return deleted;
        }

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        private User GetUserByUserIdentity(string userIdentity)
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

            return user;
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
