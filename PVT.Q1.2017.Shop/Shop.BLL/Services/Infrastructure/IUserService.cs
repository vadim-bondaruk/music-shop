namespace Shop.BLL.Services.Infrastructure
{
    using Shop.Common.Models;
    
    /// <summary>
    /// The user service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creating new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        bool RegisterUser(User user);
    }
}
