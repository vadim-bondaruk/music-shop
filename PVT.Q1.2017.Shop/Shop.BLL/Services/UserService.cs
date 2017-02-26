namespace Shop.BLL.Services
{
    using DAL.Repositories;
    using Ship.Infrastructure.Services;
    using Ship.Infrastructure.Repositories;
    using Shop.Common.Models;
    using System.Linq;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class UserService : IService<User>
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        private bool IsLoginUnique(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login");

            if (_userRepository.GetAll(u => u.Login == login).Any())
                return false;
            return true;
        }

        private bool IsEmailUnique(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("login");

            if (_userRepository.GetAll(u => u.Email == email).Any())
                return false;
            return true;
        }
    }
}
