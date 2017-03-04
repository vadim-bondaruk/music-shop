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
    /// The album service.
    /// </summary>
    public class AlbumService : Service<IAlbumRepository, Album>, IAlbumService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public AlbumService(IFactory factory, IValidator<Album> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IAlbumService Members

        /// <summary>
        /// Adds a new album with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="artist">
        /// The artist.
        /// </param>
        /// <param name="name">
        /// The album name.
        /// </param>
        public void AddAlbum(Artist artist, string name)
        {
            ValidatorHelper.CheckArtistForNull(artist);

            var album = new Album
            {
                Name = name,
                ArtistId = artist.Id
            };

            this.Register(album);
        }

        /// <summary>
        /// Returns the album with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// The album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        public Album GetAlbumInfo(int id)
        {
            using (var repository = this.CreateRepository())
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
            ValidatorHelper.CheckAlbumForNull(album);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.AlbumId == album.Id, TrackService.TrackDefaultIncludes);
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
            ValidatorHelper.CheckAlbumForNull(album);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.AlbumId == album.Id && !t.TrackPrices.Any(), TrackService.TrackDefaultIncludes);
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
            ValidatorHelper.CheckAlbumForNull(album);

            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.AlbumId == album.Id && t.TrackPrices.Any(), TrackService.TrackDefaultIncludes);
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
            using (var repository = this.CreateRepository())
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
            using (var repository = this.CreateRepository())
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
            using (var repository = this.CreateRepository())
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
            ValidatorHelper.CheckAlbumForNull(album);
            ValidatorHelper.CheckPriceLevelForNull(priceLevel);

            using (var repository = this.Factory.Create<IAlbumPriceRepository>())
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
            ValidatorHelper.CheckAlbumForNull(album);

            using (var repository = this.Factory.Create<IAlbumPriceRepository>())
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