namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// The track validator.
    /// </summary>
    public class TrackValidator : IValidator<Track>
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
        /// Determines whether the track name is valid.
        /// </summary>
        /// <param name="trackName">
        /// The track name.
        /// </param>
        /// <returns>
        /// <b>true</b> if track name is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsTrackNameValid(string trackName)
        {
            return !string.IsNullOrWhiteSpace(trackName);
        }

        /// <summary>
        /// Validates the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="track"/> or artist or album is null.
        /// </exception>
        /// <exception cref="InvalidTrackException">
        /// When the <paramref name="track"/> is invalid.
        /// </exception>
        public void Validate(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (!IsTrackNameValid(track.Name))
            {
                throw new InvalidTrackException("Invalid track name specified.");
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
        public bool IsValid(Track track)
        {
            if (track == null)
            {
                return false;
            }

            if (!IsTrackNameValid(track.Name))
            {
                return false;
            }

            ArtistValidator artistValidator = new ArtistValidator();
            if (!artistValidator.IsValid(track.Artist))
            {
                return false;
            }

            if (track.Album != null)
            {
                AlbumValidator albumValidator = new AlbumValidator();
                if (!albumValidator.IsValid(track.Album))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion //Public Methods
    }
}