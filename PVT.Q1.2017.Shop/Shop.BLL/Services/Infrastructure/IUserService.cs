namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.Models;
    using DTO;

    /// <summary>
    /// The user service (have to be extended by UserMenagement team).
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Returns the user with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>
        /// The user with the specified <paramref name="id"/> or <b>null</b> if user doesn't exist.
        /// </returns>
        User GetUserInfo(int id);

        /// <summary>
        /// Returns all feedbacks that the specified <paramref name="user"/> have ever made.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// All feedbacks that the specified <paramref name="user"/> have ever made.
        /// </returns>
        ICollection<Feedback> GetUserFeedbacksList(User user);

        /// <summary>
        /// Returns all votes that the specified <paramref name="user"/> have ever made.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// All votes that the specified <paramref name="user"/> have ever made.
        /// </returns>
        ICollection<Vote> GetUserVotesList(User user);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool RegisterUser(UserDTO user);
    }
}