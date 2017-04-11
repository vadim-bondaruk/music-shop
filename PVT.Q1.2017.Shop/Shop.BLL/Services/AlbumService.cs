namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Shop.BLL.Helpers;
    using Shop.BLL.Services.Infrastructure;
    using Shop.Common.Models;
    using Shop.Common.ViewModels;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The album service.
    /// </summary>
    public class AlbumService : BaseService, IAlbumService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumService" /> class.
        /// </summary>
        /// <param name="factory">
        ///     The repository factory.
        /// </param>
        public AlbumService(IRepositoryFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        ///     Returns the album details using the specified currency and price level for album price.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevelId">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     The information about album with the specified <paramref name="id" /> or <b>null</b> if album doesn't exist.
        /// </returns>
        public AlbumDetailsViewModel GetAlbumDetails(int id, int? currencyCode = null, int? priceLevelId = null)
        {
            Album album;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                album = repository.GetById(id, a => a.Artist);
            }

            var albumViewModel = ModelsMapper.GetAlbumDetailsViewModel(album);

            if (currencyCode == null)
            {
                currencyCode = ServiceHelper.GetDefaultCurrency(this.Factory).Code;
            }

            if (priceLevelId == null)
            {
                priceLevelId = ServiceHelper.GetDefaultPriceLevel(this.Factory);
            }

            using (var repository = this.Factory.GetAlbumPriceRepository())
            {
                using (var currencyRatesrepository = this.Factory.GetCurrencyRateRepository())
                {
                    albumViewModel.Price = PriceHelper.GetAlbumPrice(
                        repository,
                        currencyRatesrepository,
                        id,
                        currencyCode.Value,
                        priceLevelId.Value);
                }
            }

            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                albumViewModel.TracksCount = repository.Count(r => r.AlbumId == albumViewModel.Id);
            }

            return albumViewModel;
        }

        /// <summary>
        /// Returns all tracks whitch could be added to the specified album using the specified currency and price level for track price.
        /// </summary>
        /// <param name="albumId">
        /// The album id.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        /// </returns>
        public AlbumTracksListViewModel GetTracksToAdd(int albumId, int? currencyCode = null, int? priceLevel = null)
        {
            AlbumTracksListViewModel albumTracksListViewModel = this.CreateAlbumTracksListViewModel(albumId);

            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                albumTracksListViewModel.TracksCount = repository.Count(r => r.AlbumId == albumId);
            }

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                if (albumTracksListViewModel.Artist == null)
                {
                    tracks = repository.GetAll(
                                               t => (t.OwnerId == null || t.OwnerId == albumTracksListViewModel.OwnerId) &&
                                                    (!t.Albums.Any() || t.Albums.All(r => r.AlbumId != albumId)),
                                               t => t.Artist);
                }
                else
                {
                    tracks = repository.GetAll(
                                               t => t.ArtistId == albumTracksListViewModel.Artist.Id &&
                                                    (t.OwnerId == null || t.OwnerId == albumTracksListViewModel.OwnerId) &&
                                                    (!t.Albums.Any() || t.Albums.All(r => r.AlbumId != albumId)),
                                               t => t.Artist);
                }
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel);
            return albumTracksListViewModel;
        }

        /// <summary>
        /// Returns all registered tracks for the specified album using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered albums.
        /// </returns>
        public ICollection<AlbumViewModel> GetAlbumsList(int? currencyCode = null, int? priceLevel = null)
        {
            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.Artist);
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel);
            foreach (var trackViewModel in albumTracksListViewModel.Tracks)
            {
                trackViewModel.AlbumId = albumId;
            }

            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Returns all albums without price configured.
        /// </summary>
        /// <returns>
        ///     All albums without price configured.
        /// </returns>
        public ICollection<AlbumViewModel> GetAlbumsWithoutPrice()
        {
            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => !a.AlbumPrices.Any(), a => a.Artist);
            }

            return albums.Select(ModelsMapper.GetAlbumViewModel).ToList();
            albumTracksListViewModel.Tracks = albumTrackRelations.Select(r => ModelsMapper.GetTrackViewModel(r.Track)).ToList();
            foreach (var trackViewModel in albumTracksListViewModel.Tracks)
            {
                if (trackViewModel != null)
                {
                    trackViewModel.AlbumId = albumId;
                }
            }

            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Returns all albums with price specified using the specified currency and price level for album price.
        /// </summary>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All albums with price specified.
        /// </returns>
        public ICollection<AlbumViewModel> GetAlbumsWithPrice(int? currencyCode = null, int? priceLevel = null)
        {
            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.AlbumPrices.Any(), a => a.Artist);
            }

            return ServiceHelper.ConvertToAlbumViewModels(this.Factory, albums, currencyCode, priceLevel);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerable<AlbumDetailsViewModel> GetAllViewModels()
        {
            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.Artist);
            }

            return albums.Select(ModelsMapper.GetAlbumDetailsViewModel).ToList();
            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel);
            foreach (var trackViewModel in albumTracksListViewModel.Tracks)
            {
                trackViewModel.AlbumId = albumId;
            }

            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Returns all registered tracks for the specified album using the specified currency and price level for track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered tracks with price for the specified album.
        /// </returns>
        public AlbumTracksListViewModel GetTracksList(int albumId, int? currencyCode = null, int? priceLevel = null)
        {
            var albumTracksListViewModel = this.CreateAlbumTracksListViewModel(albumId);

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                tracks =
                    repository.GetAll(r => r.AlbumId == albumId, r => r.Track, r => r.Track.Artist)
                        .Select(r => r.Track)
                        .ToList();
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(
                this.Factory,
                tracks,
                currencyCode,
                priceLevel);
            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Returns all tracks for the specified album without price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        ///     All tracks for the specified album without price.
        /// </returns>
        public AlbumTracksListViewModel GetTracksWithoutPrice(int albumId)
        {
            var albumTracksListViewModel = this.CreateAlbumTracksListViewModel(albumId);

            ICollection<AlbumTrackRelation> albumTrackRelations;
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                albumTrackRelations = repository.GetAll(
                    r => r.AlbumId == albumId && !r.Track.TrackPrices.Any(),
                    r => r.Track);
            }

            albumTracksListViewModel.Tracks =
                albumTrackRelations.Select(r => ModelsMapper.GetTrackViewModel(r.Track)).ToList();
            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Returns all tracks for the specified album with price specified using the specified currency and price level for
        ///     track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All tracks for the specified album without price specified.
        /// </returns>
        public AlbumTracksListViewModel GetTracksWithPrice(
            int albumId,
            int? currencyCode = null,
            int? priceLevel = null)
        {
            var albumTracksListViewModel = this.CreateAlbumTracksListViewModel(albumId);

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                tracks =
                    repository.GetAll(r => r.AlbumId == albumId && r.Track.TrackPrices.Any(), t => t.Track)
                        .Select(r => r.Track)
                        .ToList();
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(
                this.Factory,
                tracks,
                currencyCode,
                priceLevel);
            return albumTracksListViewModel;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="AlbumTracksListViewModel" /> type.
        /// </summary>
        /// <param name="albumId">
        ///     The album id.
        /// </param>
        /// <returns>
        ///     A new instance of the <see cref="AlbumTracksListViewModel" /> type
        /// </returns>
        private AlbumTracksListViewModel CreateAlbumTracksListViewModel(int albumId)
        {
            Album album;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                album = repository.GetById(albumId, a => a.Artist);
            }

            return ModelsMapper.GetAlbumTracksListViewModel(album);
        }
    }
}