// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackValidator.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The track validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.BLL.Validators
{
    #region

    using System;

    using Shop.BLL.Exceptions;
    using Shop.Common.Models;
    using Shop.Infrastructure.Validators;

    #endregion

    /// <summary>
    ///     The track validator.
    /// </summary>
    public class TrackValidator : IValidator<Track>
    {
        /// <summary>
        ///     The album validator.
        /// </summary>
        private readonly IValidator<Album> _albumValidator;

        /// <summary>
        ///     The artist validator.
        /// </summary>
        private readonly IValidator<Artist> _artistValidator;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="TrackValidator" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="artistValidator">The artist validator.</param>
        /// <param name="albumValidator">The album validator.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     When <paramref name="artistValidator" /> or
        ///     <paramref name="albumValidator" /> is null.
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

        /// <summary>
        ///     Determines whether the track name is valid.
        /// </summary>
        /// <param name="trackName">The track name.</param>
        /// <returns>
        ///     <para>
        ///         <b>
        ///             <see langword="true" />
        ///         </b>
        ///     </para>
        ///     <para>
        ///         if track name is valid; otherwise
        ///         <b>
        ///             <see langword="false" />
        ///         </b>
        ///     </para>
        ///     <para>.</para>
        /// </returns>
        public bool IsTrackNameValid(string trackName)
        {
            return !string.IsNullOrWhiteSpace(trackName);
        }

        /// <summary>
        ///     Deterines whether the <paramref name="track" /> is valid.
        /// </summary>
        /// <param name="track">The track to verify.</param>
        /// <returns>
        ///     <para>
        ///         <b>
        ///             <see langword="true" />
        ///         </b>
        ///     </para>
        ///     <para>
        ///         if <paramref name="track" /> is valid; otherwise
        ///         <b>
        ///             <see langword="false" />
        ///         </b>
        ///     </para>
        ///     <para>.</para>
        /// </returns>
        public bool IsValid(Track track)
        {
            if (track == null)
            {
                return false;
            }

            if (!this.IsTrackNameValid(track.Name))
            {
                return false;
            }

            var artistValidator = new ArtistValidator();
            if (!artistValidator.IsValid(track.Artist))
            {
                return false;
            }

            if (track.Album != null)
            {
                var albumValidator = new AlbumValidator();
                if (!albumValidator.IsValid(track.Album))
                {
                    return false;
                }
            }

            {
                return true;
            }
        }

        /// <summary>
        ///     Validates the specified <paramref name="track" /> .
        /// </summary>
        /// <param name="track">The track to validate.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     When <paramref name="track" /> or artist or album is null.
        /// </exception>
        /// <exception cref="Shop.BLL.Exceptions.InvalidTrackException">
        ///     When the <paramref name="track" /> is invalid.
        /// </exception>
        public void Validate(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (!this.IsTrackNameValid(track.Name))
            {
                throw new InvalidTrackException("Invalid track name specified.");
            }

            this._artistValidator.Validate(track.Artist);
            this._albumValidator.Validate(track.Album);
        }
    }
}