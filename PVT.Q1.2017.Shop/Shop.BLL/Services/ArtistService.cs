namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The artist service
    /// </summary>
    public class ArtistService : BaseService, IArtistService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public ArtistService(IRepositoryFactory factory) : base(factory)
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
            using (var repository = this.Factory.GetArtistRepository())
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
            using (var repository = this.Factory.GetArtistRepository())
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
            using (var repository = this.Factory.GetAlbumRepository())
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
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id, TrackService.TrackDefaultIncludes);
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
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id && !t.TrackPrices.Any(), TrackService.TrackDefaultIncludes);
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
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.ArtistId == artist.Id && t.TrackPrices.Any(), TrackService.TrackDefaultIncludes);
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
            using (var repository = this.Factory.GetAlbumRepository())
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
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetAll(a => a.ArtistId == artist.Id && a.AlbumPrices.Any(), a => a.Artist);
            }
        }

        #endregion //IArtistService Members
    }
}