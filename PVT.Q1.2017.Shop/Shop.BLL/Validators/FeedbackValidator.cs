namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Helpers;
    using Infrastructure.Validators;
    using Services.Infrastructure;

    /// <summary>
    /// The feedback validator.
    /// </summary>
    public class FeedbackValidator : IValidator<Feedback>
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
        public FeedbackValidator(ITrackService trackService, IUserService userService)
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
        /// Determines whether the <paramref name="comment"/> is valid.
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="comment"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsCommentValid(string comment)
        {
            return !string.IsNullOrWhiteSpace(comment);
        }

        #endregion //Public Static Methods

        #region IValidator<Feedback> Members

        /// <summary>
        /// Validates the specified <paramref name="feedback"/>.
        /// </summary>
        /// <param name="feedback">
        /// The feedback to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="feedback"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="feedback"/> comment is invalid.
        /// </exception>
        /// <exception cref="EntityNotFoundException">
        /// When the track or user doesn't exist.
        /// </exception>
        public virtual void Validate(Feedback feedback)
        {
            ValidatorHelper.CheckFeedbackForNull(feedback);
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            if (!IsCommentValid(feedback.Comments))
            {
                throw new InvalidEntityException("Invalid comment specified.");
            }

            if (!this._trackService.IsRegistered(feedback.TrackId))
            {
                throw new EntityNotFoundException($"Track with id='{ feedback.TrackId }' doesn't exist.");
            }

            if (!this._userService.IsRegistered(feedback.UserId))
            {
                throw new EntityNotFoundException($"User with id='{ feedback.UserId }' doesn't exist.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="feedback"/> is valid.
        /// </summary>
        /// <param name="feedback">
        /// The feedback to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="feedback"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(Feedback feedback)
        {
            if (feedback == null)
            {
                return false;
            }

            if (!IsCommentValid(feedback.Comments))
            {
                return false;
            }

            if (!this._trackService.IsRegistered(feedback.TrackId))
            {
                return false;
            }

            if (!this._userService.IsRegistered(feedback.UserId))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<Feedback> Members
    }
}