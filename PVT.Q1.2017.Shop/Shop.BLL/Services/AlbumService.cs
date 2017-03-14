namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;
    using ViewModels;

    /// <summary>
    /// The album service.
    /// </summary>
    public class AlbumService : BaseService, IAlbumService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public AlbumService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Returns the album with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// The album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        public AlbumViewModel GetAlbumInfo(int id)
        {
            Album album;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                album = repository.GetById(id, a => a.Artist);
            }

            return this.CreateAlbumViewModel(album);
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
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                return repository.GetAll(r => r.AlbumId == album.Id, r => r.Track).Select(r => r.Track).ToList();
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
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                return repository.GetAll(
                                         r => r.AlbumId == album.Id &&
                                              !r.Track.TrackPrices.Any(),
                                         r => r.Track)
                                 .Select(r => r.Track).ToList();
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
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                return repository.GetAll(
                                         r => r.AlbumId == album.Id &&
                                              r.Track.TrackPrices.Any(),
                                         t => t.Track)
                                 .Select(r => r.Track).ToList();
            }
        }

        /// <summary>
        /// Returns all registered albums.
        /// </summary>
        /// <returns>
        /// All registered albums.
        /// </returns>
        public ICollection<AlbumViewModel> GetAlbumsList()
        {
            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.Artist);
            }

            return this.CreateAlbumViewModels(albums);
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

        /// <summary>
        /// Creates a new instance of the <see cref="AlbumViewModel"/> using the specified <paramref name="album"/>.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="AlbumViewModel"/> using the specified <paramref name="album"/>.
        /// </returns>
        private AlbumViewModel CreateAlbumViewModel(Album album)
        {
            var albumViewModel = Mapper.Map<AlbumViewModel>(album);
            if (album.Artist != null)
            {
                albumViewModel.ArtistName = album.Artist.Name;
            }

            // TODO: fill album price by currency and price level
            return albumViewModel;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ICollection{AlbumViewModel}"/> using the specified list of <paramref name="albums"/>.
        /// </summary>
        /// <param name="albums">
        /// The list of albums.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="ICollection{AlbumViewModel}"/> using the specified list of <paramref name="albums"/>.
        /// </returns>
        private ICollection<AlbumViewModel> CreateAlbumViewModels(ICollection<Album> albums)
        {
            List<AlbumViewModel> albumViewModels = new List<AlbumViewModel>();
            foreach (var album in albums)
            {
                var albumViewModel = this.CreateAlbumViewModel(album);
                albumViewModels.Add(albumViewModel);
            }

            return albumViewModels;
        }
    }
}