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

        /// <summary>
        /// Returns the Id by his login or password
        /// </summary>
        /// <param name="userIdentity">user login or email</param>
        /// <returns></returns>
        int GetIdOflogin(string userIdentity);

        /// <summary>
        /// Updates user model data by Id
        /// </summary>
        /// <param name="user">model user</param>
        /// <returns></returns>
        bool UpdatePersonal(User user, int Id);

        /// <summary>
        /// Returns the email by his login or password
        /// </summary>
        /// <param name="userIdentity">user login or email</param>
        /// <returns></returns>
        string GetEmailByUserIdentity(string userIdentity);

        /// <summary>
        /// Update user password data by Id
        /// </summary>
        /// <param name="id">Id from model user</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        bool UpdatePassword(int id, string newPassword);

        /// <summary>
        /// Update user password data by Id
        /// </summary>
        /// <param name="id">Id from model user</param>
        /// <param name="newPassword">new password</param>
        /// <param name="oldPassword">old password</param>
        /// <returns></returns>
        bool UpdatePassword(int id, string newPassword, string oldPassword);
    }
}
