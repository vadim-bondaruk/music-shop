namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// The track validator.
    /// </summary>
    public class TrackValidator : NamedEntityValidator<Track>
    {
        #region Fields

        /// <summary>
        /// The artist validator.
        /// </summary>
        private readonly IValidator<Artist> _artistValidator;

        /// <summary>
        /// The album validator.
        /// </summary>
        private readonly IValidator<Album> _albumValidator;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackValidator"/> class.
        /// </summary>
        /// <param name="artistValidator">
        /// The artist validator.
        /// </param>
        /// <param name="albumValidator">
        /// The album validator.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="artistValidator"/> or <paramref name="albumValidator"/> is null.
        /// </exception>
        public TrackValidator(IValidator<Artist> artistValidator, IValidator<Album> albumValidator)
        {
            if (artistValidator == null)
            {
                throw new ArgumentNullException(nameof(artistValidator));
            }

            if (albumValidator == null)
            {
                throw new ArgumentNullException(nameof(albumValidator));
            }

            this._artistValidator = artistValidator;
            this._albumValidator = albumValidator;
        }

        #endregion //Constructors

        #region Public Methods

        /// <summary>
        /// Validates the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="track"/> or artist or album is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="track"/> is invalid.
        /// </exception>
        public override void Validate(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (!TrackValidator.IsNameValid(track.Name))
            {
                throw new InvalidEntityException("Invalid track name specified.");
            }

            this._artistValidator.Validate(track.Artist);
            this._albumValidator.Validate(track.Album);
        }

        /// <summary>
        /// Determines whether the <paramref name="track"/> is valid.
        /// </summary>
        /// <param name="track">
        /// The track to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if track is valid; otherwise <b>false</b>.
        /// </returns>
        public override bool IsValid(Track track)
        {
            if (!base.IsValid(track))
            {
                return false;
            }

            if (!this._artistValidator.IsValid(track.Artist))
            {
                return false;
            }

            if (!this._albumValidator.IsValid(track.Album))
            {
                return false;
            }

            return true;
        }

        #endregion //Public Methods
    }
}