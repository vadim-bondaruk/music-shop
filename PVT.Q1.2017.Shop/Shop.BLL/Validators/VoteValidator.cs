namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;
    using Services.Infrastructure;

    /// <summary>
    /// The vote validator.
    /// </summary>
    public class VoteValidator : IValidator<Vote>
    {
        #region Fields

        /// <summary>
        /// The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService _userService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackValidator"/> class.
        /// </summary>
        /// <param name="trackService">
        /// The track service.
        /// </param>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public VoteValidator(ITrackService trackService, IUserService userService)
        {
            if (trackService == null)
            {
                throw new ArgumentNullException(nameof(trackService));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            this._trackService = trackService;
            this._userService = userService;
        }

        #endregion //Constructors

        #region Public Static Methods

        /// <summary>
        /// Determines whether the <paramref name="mark"/> is valid.
        /// </summary>
        /// <param name="mark">
        /// The mark.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="mark"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsMarkValid(Mark mark)
        {
            return Enum.IsDefined(typeof(Mark), mark);
        }

        #endregion //Public Static Methods

        #region IValidator<Vote> Members

        /// <summary>
        /// Validates the specified <paramref name="vote"/>.
        /// </summary>
        /// <param name="vote">
        /// The vote to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="vote"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="vote"/> mark is invalid.
        /// </exception>
        /// <exception cref="EntityNotFoundException">
        /// When the track or user doesn't exist.
        /// </exception>
        public virtual void Validate(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException(nameof(vote));
            }

            if (!IsMarkValid(vote.Mark))
            {
                throw new InvalidEntityException("Invalid mark specified.");
            }

            if (!this._trackService.IsRegistered(vote.TrackId))
            {
                throw new EntityNotFoundException($"Track with id='{ vote.TrackId }' doesn't exist.");
            }

            if (!this._userService.IsRegistered(vote.UserId))
            {
                throw new EntityNotFoundException($"User with id='{ vote.UserId }' doesn't exist.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="vote"/> is valid.
        /// </summary>
        /// <param name="vote">
        /// The vote to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="vote"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(Vote vote)
        {
            if (vote == null)
            {
                return false;
            }

            if (!IsMarkValid(vote.Mark))
            {
                return false;
            }

            if (!this._trackService.IsRegistered(vote.TrackId))
            {
                return false;
            }

            if (!this._userService.IsRegistered(vote.UserId))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<Vote> Members
    }
}