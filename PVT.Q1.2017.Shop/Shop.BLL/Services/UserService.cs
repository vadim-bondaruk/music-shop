namespace Shop.BLL.Services
{
    using System;
    using DTO;
    using Exceptions;
    using Infrastructure;
    using Ship.Infrastructure.Repositories;
    using Shop.BLL.Validators;
    using Shop.Common.Models;
    using Shop.Infrastructure.Enums;
    using Utils;

    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        private IRepository<User> _userRepository;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IRepository<User> userRepository)
        {            
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Addition new user to userRepository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }

            if (!UserDataValidator.IsLoginUnique(user.Login, this._userRepository))
            {
                throw new UserValidationException("User with the same login already exists", "Login");
            }

            if (!UserDataValidator.IsEmailUnique(user.Email, this._userRepository))
            {
                throw new UserValidationException("User with the same email already exists", "Email");
            }

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>()
                            .ForMember("UserRole", opt => opt.MapFrom(userDTO => UserRoles.User))
                            .ForMember("Password", opt => opt.MapFrom(userDTO => PasswordEncryptor.GetHashString(userDTO.Password))));
            var userDB = AutoMapper.Mapper.Map<User>(user);
            this._userRepository.AddOrUpdate(userDB);
            return true;
        }     
    }
}
