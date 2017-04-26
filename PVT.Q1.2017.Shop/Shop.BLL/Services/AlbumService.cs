﻿namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using Common.ViewModels;
    using DAL.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure.Models;

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
        /// Returns the album details using the specified currency and price level for album price.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevelId">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// The information about album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        public AlbumDetailsViewModel GetAlbumDetails(int id, int? currencyCode = null, int? priceLevelId = null, int? userId = null)
        {
            Album album;
            using (var repository = Factory.GetAlbumRepository())
            {
                album = repository.GetById(id, a => a.Artist);
            }

            var albumViewModel = ModelsMapper.GetAlbumDetailsViewModel(album);

            if (currencyCode == null)
            {
                currencyCode = ServiceHelper.GetDefaultCurrency(Factory).Code;
            }

            if (priceLevelId == null)
            {
                priceLevelId = ServiceHelper.GetDefaultPriceLevel(Factory);
            }

            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                albumViewModel.TracksCount = repository.Count(r => r.AlbumId == albumViewModel.Id);
                if (albumViewModel.TracksCount > 0)
                {
                    using (var albumPriceRepository = Factory.GetAlbumPriceRepository())
                    {
                        using (var currencyRatesrepository = Factory.GetCurrencyRateRepository())
                        {
                            albumViewModel.Price =
                                PriceHelper.GetAlbumPrice(albumPriceRepository, currencyRatesrepository, id, currencyCode.Value, priceLevelId.Value);
                        }
                    }
                }
            }

            if (userId != null)
            {
                using (var repository = Factory.GetOrderAlbumRepository())
                {
                    albumViewModel.IsOrdered =
                            repository.Exist(o => o.UserId == userId && o.AlbumId == albumViewModel.Id);
                }

                using (var repository = Factory.GetPurchasedAlbumRepository())
                {
                    albumViewModel.IsPurchased =
                            repository.Exist(p => p.UserId == userId && p.AlbumId == albumViewModel.Id);
                }
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
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// </returns>
        public AlbumTracksListViewModel GetTracksToAdd(int albumId, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            AlbumTracksListViewModel albumTracksListViewModel = CreateAlbumTracksListViewModel(albumId, currencyCode, priceLevel, userId);

            ICollection<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                decimal ownerId = albumTracksListViewModel.AlbumDetails.OwnerId;
                if (albumTracksListViewModel.AlbumDetails.Artist == null)
                {
                    tracks = repository.GetAll(
                                               t => (t.OwnerId == null || t.OwnerId == ownerId) &&
                                                    t.Albums.All(r => r.AlbumId != albumId),
                                               t => t.Artist);
                }
                else
                {
                    tracks = repository.GetAll(
                                               t => t.ArtistId == albumTracksListViewModel.AlbumDetails.Artist.Id &&
                                                    (t.OwnerId == null || t.OwnerId == ownerId) &&
                                                    t.Albums.All(r => r.AlbumId != albumId),
                                               t => t.Artist);
                }
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(Factory, tracks, currencyCode, priceLevel, userId);

            return albumTracksListViewModel;
        }

        /// <summary>
        /// Returns all registered tracks for the specified album using the specified currency and price level for track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered tracks with price for the specified album.
        /// </returns>
        public AlbumTracksListViewModel GetTracks(int albumId, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            AlbumTracksListViewModel albumTracksListViewModel = CreateAlbumTracksListViewModel(albumId, currencyCode, priceLevel, userId);

            ICollection<Track> tracks;
            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                tracks = repository.GetAll(r => r.AlbumId == albumId, r => r.Track, r => r.Track.Artist).Select(r => r.Track).ToList();
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(Factory, tracks, currencyCode, priceLevel, userId);
            foreach (var trackViewModel in albumTracksListViewModel.Tracks)
            {
                trackViewModel.AlbumId = albumId;
            }

            return albumTracksListViewModel;
        }

        /// <summary>
        /// Returns all tracks for the specified album without price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        /// All tracks for the specified album without price.
        /// </returns>
        public AlbumTracksListViewModel GetTracksWithoutPrice(int albumId)
        {
            AlbumTracksListViewModel albumTracksListViewModel = CreateAlbumTracksListViewModel(albumId);

            ICollection<AlbumTrackRelation> albumTrackRelations;
            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                albumTrackRelations = repository.GetAll(
                                           r => r.AlbumId == albumId &&
                                                !r.Track.TrackPrices.Any(),
                                           r => r.Track);
            }

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
        /// Returns all tracks for the specified album with price specified using the specified currency and price level for track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All tracks for the specified album without price specified.
        /// </returns>
        public AlbumTracksListViewModel GetTracksWithPrice(int albumId, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            AlbumTracksListViewModel albumTracksListViewModel = CreateAlbumTracksListViewModel(albumId, currencyCode, priceLevel, userId);

            ICollection<Track> tracks;
            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                tracks = repository.GetAll(
                                           r => r.AlbumId == albumId &&
                                                r.Track.TrackPrices.Any(),
                                           t => t.Track)
                                   .Select(r => r.Track).ToList();
            }

            albumTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(Factory, tracks, currencyCode, priceLevel, userId);
            foreach (var trackViewModel in albumTracksListViewModel.Tracks)
            {
                trackViewModel.AlbumId = albumId;
            }

            return albumTracksListViewModel;
        }

        /// <summary>
        /// Returns all registered albums using the specified currency and price level for album price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered albums.
        /// </returns>
        public ICollection<AlbumViewModel> GetAlbums(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.Artist);
            }

            return ServiceHelper.ConvertToAlbumViewModels(Factory, albums, currencyCode, priceLevel, userId);
        }

        /// <summary>
        /// Returns all registered albums using the specified currency and price level for album price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered albums.
        /// </returns>
        public PagedResult<AlbumViewModel> GetAlbums(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(page, pageSize, a => a.Artist);
            }

            var albumViewModels = ServiceHelper.ConvertToAlbumViewModels(Factory, albums.Items, currencyCode, priceLevel, userId);
            return new PagedResult<AlbumViewModel>(albumViewModels, pageSize, page, albums.TotalItemsCount);
        }

        /// <summary>
        /// Returns all registered albums detailed information using the specified currency and price level for album price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered albums detailed information.
        /// </returns>
        public ICollection<AlbumDetailsViewModel> GetDetailedAlbumsList(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll();
            }

            List<AlbumDetailsViewModel> detailedList = new List<AlbumDetailsViewModel>();
            foreach (var album in albums)
            {
                detailedList.Add(GetAlbumDetails(album.Id, currencyCode, priceLevel, userId));
            }

            return detailedList;
        }

        /// <summary>
        /// Returns all registered albums detailed information using the specified currency and price level for album price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered albums detailed information.
        /// </returns>
        public PagedResult<AlbumDetailsViewModel> GetDetailedAlbumsList(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(page, pageSize);
            }

            List<AlbumDetailsViewModel> detailedList = new List<AlbumDetailsViewModel>();
            foreach (var album in albums.Items)
            {
                detailedList.Add(GetAlbumDetails(album.Id, currencyCode, priceLevel, userId));
            }

            return new PagedResult<AlbumDetailsViewModel>(detailedList, pageSize, page, albums.TotalItemsCount);
        }

        /// <summary>
        /// Returns all albums without price configured.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <returns>
        /// All albums without price configured.
        /// </returns>
        public PagedResult<AlbumViewModel> GetAlbumsWithoutPrice(int page, int pageSize)
        {
            PagedResult<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(page, pageSize, a => !a.AlbumPrices.Any(), a => a.Artist);
            }

            var albumViewModels = albums.Items.Select(ModelsMapper.GetAlbumViewModel).ToList();
            return new PagedResult<AlbumViewModel>(albumViewModels, pageSize, page, albums.TotalItemsCount);
        }

        /// <summary>
        /// Returns all albums with price specified using the specified currency and price level for album price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All albums with price specified.
        /// </returns>
        public PagedResult<AlbumViewModel> GetAlbumsWithPrice(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Album> albums;
            using (var repository = Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(page, pageSize, a => a.AlbumPrices.Any(), a => a.Artist);
            }

            var albumViewModels = ServiceHelper.ConvertToAlbumViewModels(Factory, albums.Items, currencyCode, priceLevel, userId);
            return new PagedResult<AlbumViewModel>(albumViewModels, pageSize, page, albums.TotalItemsCount);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AlbumTracksListViewModel"/> type.
        /// </summary>
        /// <param name="albumId">
        /// The album id.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="AlbumTracksListViewModel"/> type
        /// </returns>
        private AlbumTracksListViewModel CreateAlbumTracksListViewModel(int albumId, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            return new AlbumTracksListViewModel
            {
                AlbumDetails = GetAlbumDetails(albumId, currencyCode, priceLevel, userId)
            };
        }
    }
}