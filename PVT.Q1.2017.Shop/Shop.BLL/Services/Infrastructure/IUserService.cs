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
        /// Returns the Id by his login or password
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        int GetIdOflogin(string userIdentity);

        /// <summary>
        /// Updates user model data by Id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdatePersonal(User user, int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        string GetEmailByUserIdentity(string userIdentity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        bool UpdatePassword(int id, string newPassword, string oldPassword);
    }
}
