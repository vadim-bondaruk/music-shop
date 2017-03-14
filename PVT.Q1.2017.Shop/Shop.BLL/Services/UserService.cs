namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The user service (have to be extended by UserMenagement team).
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public UserService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Returns the user with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>
        /// The user with the specified <paramref name="id"/> or <b>null</b> if user doesn't exist.
        /// </returns>
        public User GetUserInfo(int id)
        {
            using (var repository = this.Factory.GetUserRepository())
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
        public ICollection<Feedback> GetUserFeedbacksList(User user)
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
        public ICollection<Vote> GetUserVotesList(User user)
        {
            using (var repository = this.Factory.GetVoteRepository())
            {
                return repository.GetAll(v => v.UserId == user.Id, v => v.Track, v => v.User);
            }
        }
    }
}