namespace Shop.BLL.Services
{
    using System;
    using System.Linq;
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
        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
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
