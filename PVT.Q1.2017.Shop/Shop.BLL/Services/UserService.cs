namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The user service (have to be extended by UserMenagement team).
    /// </summary>
    public class UserService : Service<IUserRepository, User>, IUserService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public UserService(IFactory factory, IValidator<User> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IUserService Members

        /// <summary>
        /// Returns the user with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>
        /// The user with the specified <paramref name="id"/> or <b>null</b> if user doesn't exist.
        /// </returns>
        public User GetUserInfo(int id)
        {
            using (var repository = this.CreateRepository())
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
            ValidatorHelper.CheckUserForNull(user);

            using (var repository = this.Factory.Create<IFeedbackRepository>())
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
            ValidatorHelper.CheckUserForNull(user);

            using (var repository = this.Factory.Create<IVoteRepository>())
            {
                return repository.GetAll(v => v.UserId == user.Id, v => v.Track, v => v.User);
            }
        }

        #endregion //IUserService Members
    }
}