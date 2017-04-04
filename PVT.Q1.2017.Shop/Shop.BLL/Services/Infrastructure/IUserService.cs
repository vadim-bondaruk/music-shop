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

        /// <summary>
        /// Check if user already exist
        /// </summary>
        /// <param name="userIdentity"> user login or email </param>
        /// <returns></returns>
        bool IsUserExist(string userIdentity);
    }
}
