namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The album service.
    /// </summary>
    public class AlbumService : BaseService, IAlbumService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public AlbumService(IRepositoryFactory factory) : base(factory)
        {
        }

        #endregion //Constructors

        #region IAlbumService Members

        /// <summary>
        /// Returns the album with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// The album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        public Album GetAlbumInfo(int id)
        {
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetById(id, a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all registered tracks for the specified <paramref name="album"/>.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        /// All registered tracks for the specified <paramref name="album"/>.
        /// </returns>
        public ICollection<Track> GetTracksList(Album album)
        {
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.AlbumId == album.Id, t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all tracks for the specified <paramref name="album"/> without price configured.
        /// </summary>
        /// <returns>
        /// All tracks for the specified <paramref name="album"/> without price configured.
        /// </returns>
        public ICollection<Track> GetTracksWithoutPriceConfigured(Album album)
        {
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.AlbumId == album.Id && !t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all tracks for the specified <paramref name="album"/> with price specified.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="album"/> without price specified.
        /// </returns>
        public ICollection<Track> GetTracksWithPriceConfigured(Album album)
        {
            using (var repository = this.Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.AlbumId == album.Id && t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all registered albums.
        /// </summary>
        /// <returns>
        /// All registered albums.
        /// </returns>
        public ICollection<Album> GetAlbumsList()
        {
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetAll(a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all albums without price configured.
        /// </summary>
        /// <returns>
        /// All albums without price configured.
        /// </returns>
        public ICollection<Album> GetAlbumsWithoutPriceConfigured()
        {
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetAll(a => !a.AlbumPrices.Any(), a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all albums with price specified.
        /// </summary>
        /// <returns>
        /// All albums without price specified.
        /// </returns>
        public ICollection<Album> GetAlbumsWithPriceConfigured()
        {
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetAll(a => a.AlbumPrices.Any(), a => a.Artist);
            }
        }

        /// <summary>
        /// Returns all <paramref name="album"/> prices for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="priceLevel">The price level.</param>
        /// <returns>
        /// All <paramref name="album"/> prices for the specified  <paramref name="priceLevel"/>.
        /// </returns>
        public ICollection<AlbumPrice> GetAlbumPrices(Album album, PriceLevel priceLevel)
        {
            using (var repository = this.Factory.GetAlbumPriceRepository())
            {
                return repository.GetAll(
                                         p => p.AlbumId == album.Id &&
                                              p.PriceLevelId == priceLevel.Id,
                                         p => p.Currency);
            }
        }

        /// <summary>
        /// Returns all <paramref name="album"/> prices.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>All <paramref name="album"/> prices>.</returns>
        public ICollection<AlbumPrice> GetAlbumPrices(Album album)
        {
            using (var repository = this.Factory.GetAlbumPriceRepository())
            {
                return repository.GetAll(
                                         p => p.AlbumId == album.Id,
                                         p => p.Currency,
                                         p => p.PriceLevel);
            }
        }

        #endregion //IAlbumService Members
    }
}