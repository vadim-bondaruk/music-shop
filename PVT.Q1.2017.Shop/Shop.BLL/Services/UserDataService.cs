namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using Common.Models;
    using DAL.Infrastruture;
    using DTO;
    using Exceptions;
    using Infrastructure;
    using Shop.Infrastructure.Enums;
    using Utils;
    using Validators;

    /// <summary>
    /// The user service.
    /// </summary>
    public class UserDataService : BaseService, IUserDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public UserDataService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Returns the user with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>
        /// The user with the specified <paramref name="id"/> or <b>null</b> if user doesn't exist.
        /// </returns>
        public UserData GetUserData(int id)
        {
            using (var repository = this.Factory.GetUserDataRepository())
            {
                return repository.GetById(id, u => u.PriceLevel, u => u.UserCurrency);
            }
        }

        /// <summary>
        /// Returns all feedbacks that the specified <paramref name="user"/> have ever made.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// All feedbacks that the specified <paramref name="user"/> have ever made.
        /// </returns>
        public ICollection<Feedback> GetUserFeedbacksList(UserData user)
        {
            using (var repository = this.Factory.GetFeedbackRepository())
            {
                return repository.GetAll(f => f.UserId == user.Id, f => f.Track, f => f.User);
            }
        }

        /// <summary>
        /// Returns all votes that the specified <paramref name="user"/> have ever made.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// All votes that the specified <paramref name="user"/> have ever made.
        /// </returns>
        public ICollection<Vote> GetUserVotesList(UserData user)
        {
            using (var repository = this.Factory.GetVoteRepository())
            {
                return repository.GetAll(v => v.UserId == user.Id, v => v.Track, v => v.User);
            }
        }

        /* /// <summary>
        /// Addition new user to userRepository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(UserDTO user)
        {
            var registered = false;

            if (user == null)
            {
                throw new ArgumentException("user");
            }

            if (!UserDataValidator.IsLoginUnique(user.Login, this.Factory.GetUserRepository()))
            {
                throw new UserValidationException("User with the same login already exists", "Login");
            }

            if (!UserDataValidator.IsEmailUnique(user.Email, this.Factory.GetUserRepository()))
            {
                throw new UserValidationException("User with the same email already exists", "Email");
            }

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>()
                            .ForMember("UserRoles", opt => opt.MapFrom(userDTO => this.GetDefaultUserRoles()))
                            .ForMember("Password", opt => opt.MapFrom(userDTO => PasswordEncryptor.GetHashString(userDTO.Password)))
                            .ForMember("IdentityKey", opt => opt.MapFrom(userDTO => this.GetIdentityKey()))
                            .ForMember("PriceLevelId", opt => opt.MapFrom(userDTO => this.GetDefaultPriceLevelId()))
                            .ForMember("CurrencyId", opt => opt.MapFrom(userDTO => this.GetDefaultCurrencyId())));                            

            var userDB = AutoMapper.Mapper.Map<User>(user);         

            try
            {
                using (var userRepository = this.Factory.GetUserRepository())
                {
                    userRepository.AddOrUpdate(userDB);
                    userRepository.SaveChanges();
                } 

                registered = true;
            }
            catch (Exception ex)
            {
                // write data to log
                throw;
            }
           
            return registered;
        }*/

        /// <summary>
        /// Getting default user roles
        /// </summary>
        /// <returns></returns>
        private UserRoles[] GetDefaultUserRoles()
        {
            return new UserRoles[] { UserRoles.User };
        }

        /// <summary>
        /// Implement identity key receiving logic
        /// </summary>
        /// <returns></returns>
        private string GetIdentityKey()
        {
            return new string('-', 30);
        }

        /// <summary>
        /// Implenent receiving default value
        /// </summary>
        /// <returns></returns>
        private int GetDefaultCurrencyId()
        {
            return 1;
        }

        /// <summary>
        /// Implenent receiving default value
        /// </summary>
        /// <returns></returns>
        private int GetDefaultPriceLevelId()
        {
            return 1;
        }
    }
}