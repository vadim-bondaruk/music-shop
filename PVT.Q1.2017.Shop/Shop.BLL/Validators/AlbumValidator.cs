namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Services.Infrastructure;

    /// <summary>
    /// The album validator
    /// </summary>
    public class AlbumValidator : NamedEntityValidator<Album>
    {
        #region Fields

        /// <summary>
        /// The artist service.
        /// </summary>
        private readonly IArtistService _artistService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidator"/> class.
        /// </summary>
        /// <param name="artistService">The artist service.</param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="artistService"/> is null.
        /// </exception>
        public AlbumValidator(IArtistService artistService)
        {
            if (artistService == null)
            {
                throw new ArgumentNullException(nameof(artistService));
            }

            this._artistService = artistService;
        }

        #endregion //Constructors

        #region IValidator<Album> Members

        /// <summary>
        /// Validates the specified <paramref name="album"/>.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="album"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="album"/> name is invalid or artist is not specified.
        /// </exception>
        /// <exception cref="EntityNotFoundException">
        /// When the artist doesn't exist.
        /// </exception>
        public override void Validate(Album album)
        {
            base.Validate(album);

            if (!album.ArtistId.HasValue)
            {
                throw new InvalidEntityException("Invalid album. Artist is not specified.");
            }

            if (!this._artistService.IsRegistered(album.ArtistId.Value))
            {
                throw new EntityNotFoundException($"Artist with id='{ album.ArtistId }' doesn't exist.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="album"/> is valid.
        /// </summary>
        /// <param name="album">
        /// The album to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="album"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public override bool IsValid(Album album)
        {
            if (!base.IsValid(album))
            {
                return false;
            }

            if (!album.ArtistId.HasValue)
            {
                return false;
            }

            if (!this._artistService.IsRegistered(album.ArtistId.Value))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<Album> Members
    }
}