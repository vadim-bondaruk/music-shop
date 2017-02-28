namespace Shop.BLL.Services
{    
    using System;
    using System.Linq;
    using DTO;
    using Exceptions;
    using Ninject;
    using Ship.Infrastructure.Repositories;
    using Ship.Infrastructure.Services;
    using Shop.Common.Models;   

    /// <summary>
    /// 
    /// </summary>
    public class UserService : IService<User>
    {
        /// <summary>
        /// 
        /// </summary>
        private IRepository<User> _userRepository;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService()
        {
            var kernel = new StandardKernel(new BllNinjectModule());
            this._userRepository = kernel.Get<IRepository<User>>();
        }

        /// <summary>
        /// Addition new user to userrepository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(UserDTO user)
        {
            if (!this.IsLoginUnique(user.Login))
            {
                throw new UserValidationException("User with the same login already exists", "Login");
            }

            if (!this.IsEmailUnique(user.Email))
            {
                throw new UserValidationException("User with the same email already exists", "Email");
            }

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
            var userDB = AutoMapper.Mapper.Map<User>(user);
            this._userRepository.AddOrUpdate(userDB);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private bool IsLoginUnique(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException("login");
            }                

            if (this._userRepository.GetAll(u => u.Login == login).Any())
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
        private bool IsEmailUnique(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email");
            }                

            if (this._userRepository.GetAll(u => u.Email == email).Any())
            {
                return false;
            }
                
            return true;
        }
    }
}
