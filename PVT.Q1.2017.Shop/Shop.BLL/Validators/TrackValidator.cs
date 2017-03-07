namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Services.Infrastructure;

    /// <summary>
    /// The track validator.
    /// </summary>
    public class TrackValidator : NamedEntityValidator<Track>
    {
        #region Fields

        /// <summary>
        /// The album service.
        /// </summary>
        private readonly IAlbumService _albumService;

        /// <summary>
        /// The artist service.
        /// </summary>
        private readonly IArtistService _artistService;

        /// <summary>
        /// The genre service.
        /// </summary>
        private readonly IGenreService _genreService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackValidator"/> class.
        /// </summary>
        /// <param name="albumService">The album service.</param>
        /// <param name="artistService">The artist service.</param>
        /// <param name="genreService"> The genre service.</param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="albumService"/> is null
        /// or the <paramref name="artistService"/> is null
        /// or the <paramref name="genreService"/> is null.
        /// </exception>
        public TrackValidator(IAlbumService albumService, IArtistService artistService, IGenreService genreService)
        {
            if (albumService == null)
            {
                throw new ArgumentNullException(nameof(albumService));
            }

            if (artistService == null)
            {
                throw new ArgumentNullException(nameof(artistService));
            }

            if (genreService == null)
            {
                throw new ArgumentNullException(nameof(genreService));
            }

            this._albumService = albumService;
            this._artistService = artistService;
            this._genreService = genreService;
        }

        #endregion //Constructors

        #region IValidator<Track> Members

        /// <summary>
        /// Validates the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="track"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="track"/> name is invalid
        /// or artist is not specified
        /// or album is not specified
        /// or <paramref name="track"/> and album has different artists.
        /// </exception>
        /// <exception cref="EntityNotFoundException">
        /// When the artist, album or genre doesn't exist.
        /// </exception>
        public override void Validate(Track track)
        {
            base.Validate(track);

            if (!track.ArtistId.HasValue)
            {
                throw new InvalidEntityException("Invalid track. Artist is not specified.");
            }

            if (!track.AlbumId.HasValue)
            {
                throw new InvalidEntityException("Invalid track. Album is not sspecified.");
            }

            if (!this._albumService.IsRegistered(track.AlbumId.Value))
            {
                throw new EntityNotFoundException($"Album with id='{ track.AlbumId }' doesn't exist.");
            }

            if (!this._artistService.IsRegistered(track.ArtistId.Value))
            {
                throw new EntityNotFoundException($"Artist with id='{ track.ArtistId }' doesn't exist.");
            }

            if (track.GenreId.HasValue && !this._genreService.IsRegistered(track.GenreId.Value))
            {
                throw new EntityNotFoundException($"Genre with id='{ track.GenreId }' doesn't exist.");
            }

            var album = this._albumService.GetAlbumInfo(track.AlbumId.Value);
            if (album.ArtistId != track.ArtistId)
            {
                throw new InvalidEntityException($"The track and album with id='{ track.AlbumId }' has different artists.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="track"/> is valid.
        /// </summary>
        /// <param name="track">
        /// The track to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="track"/> is valid; otherwise <b>false</b>.
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

            if (!this._albumService.IsRegistered(track.AlbumId.Value))
            {
                return false;
            }

            if (!this._artistService.IsRegistered(track.ArtistId.Value))
            {
                return false;
            }

            if (track.GenreId.HasValue && !this._genreService.IsRegistered(track.GenreId.Value))
            {
                return false;
            }

            var album = this._albumService.GetAlbumInfo(track.AlbumId.Value);
            if (album.ArtistId != track.ArtistId)
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<Track> Members
    }
}