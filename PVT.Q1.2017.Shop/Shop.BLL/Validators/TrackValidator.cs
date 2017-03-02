namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;

    /// <summary>
    /// The track validator.
    /// </summary>
    public class TrackValidator : NamedEntityValidator<Track>
    {
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

            if (!track.ArtistId.HasValue)
            {
                throw new InvalidEntityException("Invalid track artist specified.");
            }

            if (!track.AlbumId.HasValue)
            {
                throw new InvalidEntityException("Invalid track album specified.");
            }
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

            if (!track.ArtistId.HasValue)
            {
                return false;
            }

            if (!track.AlbumId.HasValue)
            {
                return false;
            }

            return true;
        }

        #endregion //Public Methods
    }
}