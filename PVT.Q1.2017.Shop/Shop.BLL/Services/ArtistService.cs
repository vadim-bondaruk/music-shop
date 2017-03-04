namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The artist service
    /// </summary>
    public class ArtistService : Service<IArtistRepository, Artist>, IArtistService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public ArtistService(IFactory factory, IValidator<Artist> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IArtistService Members

        /// <summary>
        /// Returns the artist with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The artist id.</param>
        /// <returns>
        /// The artist with the specified <paramref name="id"/> or <b>null</b> if artist doesn't exist.
        /// </returns>
        public Artist GetArtistInfo(int id)
        {
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id);
            }
        }

        /// <summary>
        /// Returns all registered artists.
        /// </summary>
        /// <returns>
        /// All registered artists.
        /// </returns>
        public ICollection<Artist> GetAlbumsList()
        {
            using (var repository = this.CreateRepository())
            {
                return repository.GetAll();
            }
        }

        /// <summary>
        /// Returns all registered albums for the specified <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All registered albums for the specified <paramref name="artist"/>.
        /// </returns>
        public ICollection<Album> GetAlbumsList(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<IAlbumRepository>())
            {
                return repository.GetAll(a => a.ArtistId == artist.Id, a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all registered tracks for the specified <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All registered tracks for the specified <paramref name="artist"/>.
        /// </returns>
        public ICollection<Track> GetTracksList(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id, TrackService.DefaultIncludes);
            }
        }

        /// <summary>
        /// Returns all tracks for the specified <paramref name="artist"/> without price configured.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="artist"/> without price configured.
        /// </returns>
        public ICollection<Track> GetTracksWithoutPriceConfigured(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id && !t.TrackPrices.Any(), TrackService.DefaultIncludes);
            }
        }

        /// <summary>
        /// Returns all tracks for the specified <paramref name="artist"/> with price specified.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="artist"/> with price specified.
        /// </returns>
        public ICollection<Track> GetTracksWithPriceConfigured(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id && t.TrackPrices.Any(), TrackService.DefaultIncludes);
            }
        }

        /// <summary>
        /// Returns all albums for the specified <paramref name="artist"/> without price configured.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All albums for the specified <paramref name="artist"/> without price configured.
        /// </returns>
        public ICollection<Album> GetAlbumsWithoutPriceConfigured(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<IAlbumRepository>())
            {
                return repository.GetAll(a => a.ArtistId == artist.Id && !a.AlbumPrices.Any(), a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all albums for the specified <paramref name="artist"/> with price specified.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All albums for the specified <paramref name="artist"/> with price specified.
        /// </returns>
        public ICollection<Album> GetAlbumsWithPriceConfigured(Artist artist)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            using (var repository = this.Factory.Create<IAlbumRepository>())
            {
                return repository.GetAll(a => a.ArtistId == artist.Id && a.AlbumPrices.Any(), a => a.Artist);
            }
        }

        #endregion //IArtistService Members
    }
}