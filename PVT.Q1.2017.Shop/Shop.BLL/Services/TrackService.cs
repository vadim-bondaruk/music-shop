﻿namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common.Models;
    using Common.ViewModels;
    using DAL.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// The track service
    /// </summary>
    public class TrackService : BaseService, ITrackService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The  repositories factory.
        /// </param>
        public TrackService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Returns the track details using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevelId">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="id">The track id.</param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// The information about track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        public TrackDetailsViewModel GetTrackDetails(int id, int? currencyCode = null, int? priceLevelId = null, int? userId = null)
        {
            Track track;
            using (var repository = Factory.GetTrackRepository())
            {
                track = repository.GetById(id, t => t.Artist, t => t.Genre);
            }

            return CreateTrackDetailsViewModel(track, currencyCode, priceLevelId, userId);
        }

        /// <summary>
        /// Returns all registered tracks using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        ///     The current user id.
        /// </param>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public async Task<ICollection<TrackViewModel>> GetTracksAsync(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = await repository.GetAllAsync(t => t.Artist, t => t.Genre).ConfigureAwait(false);
            }

            // TODO: implement async version ConvertToTrackViewModelsAsync
            return ServiceHelper.ConvertToTrackViewModels(Factory, tracks, currencyCode, priceLevel, userId);
        }

        /// <summary>
        /// Returns all registered tracks using the specified currency and price level for track price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        ///     The current user id.
        /// </param>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public PagedResult<TrackViewModel> GetTracks(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(page, pageSize, t => t.Artist, t => t.Genre);
            }

            var trackViewModels = ServiceHelper.ConvertToTrackViewModels(Factory, tracks.Items, currencyCode, priceLevel, userId);
            return new PagedResult<TrackViewModel>(trackViewModels, tracks.PageSize, tracks.CurrentPage, tracks.TotalItemsCount);
        }

        /// <summary>
        /// Returns all registered tracks with detailed information using the specified currency and price level for track price.
        /// </summary>
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
        /// All registered tracks with detailed information.
        /// </returns>
        public async Task<ICollection<TrackDetailsViewModel>> GetDetailedTracksListAsync(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = await repository.GetAllAsync(t => t.Artist, t => t.Genre).ConfigureAwait(false);
            }

            List<TrackDetailsViewModel> detailedList = new List<TrackDetailsViewModel>();
            foreach (var track in tracks)
            {
                // TODO: implement asyn version of the CreateTrackDetailsViewModelAsync
                detailedList.Add(CreateTrackDetailsViewModel(track, currencyCode, priceLevel, userId));
            }

            return detailedList;
        }

        /// <summary>
        /// Returns all registered tracks with detailed information using the specified currency and price level for track price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
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
        /// All registered tracks with detailed information.
        /// </returns>
        public PagedResult<TrackDetailsViewModel> GetDetailedTracksList(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(page, pageSize, t => t.Artist, t => t.Genre);
            }

            List<TrackDetailsViewModel> detailedList = new List<TrackDetailsViewModel>();
            foreach (var track in tracks.Items)
            {
                detailedList.Add(GetTrackDetails(track.Id, currencyCode, priceLevel, userId));
            }

            return new PagedResult<TrackDetailsViewModel>(detailedList, tracks.PageSize, tracks.CurrentPage, tracks.TotalItemsCount);
        }

        /// <summary>
        /// Returns all tracks which don't have price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        public PagedResult<TrackViewModel> GetTracksWithoutPrice(int page, int pageSize)
        {
            PagedResult<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(page, pageSize, t => !t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }

            var trackViewModels = tracks.Items.Select(ModelsMapper.GetTrackViewModel).ToList();
            return new PagedResult<TrackViewModel>(trackViewModels, pageSize, page, tracks.TotalItemsCount);
        }

        /// <summary>
        /// Returns all tracks with price specified using the specified currency and price level for track price.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
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
        /// All tracks with price specified.
        /// </returns>
        public PagedResult<TrackViewModel> GetTracksWithPrice(int page, int pageSize, int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            PagedResult<Track> tracks;
            using (var repository = Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(page, pageSize, t => t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }

            var tracksViewModels = ServiceHelper.ConvertToTrackViewModels(Factory, tracks.Items, currencyCode, priceLevel, userId);
            return new PagedResult<TrackViewModel>(tracksViewModels, pageSize, page, tracks.TotalItemsCount);
        }

        /// <summary>
        /// Returns all albums which contain the specified track using the specified currency and price level.
        /// </summary>
        /// <param name="trackId">The track id.</param>
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
        /// All albums which contain the specified track.
        /// </returns>
        public TrackAlbumsListViewModel GetAlbums(int trackId, int? currencyCode = null, int? priceLevelId = null, int? userId = null)
        {
            TrackAlbumsListViewModel trackAlbumsListViewModel = CreateTrackAlbumsListViewModel(trackId, currencyCode, priceLevelId, userId);

            if (trackAlbumsListViewModel.TrackDetails.AlbumsCount > 0)
            {
                ICollection<Album> albums;
                using (var repository = Factory.GetAlbumRepository())
                {
                    albums = repository.GetAll(a => a.Tracks.Any(t => t.TrackId == trackId), a => a.Artist);
                }

                trackAlbumsListViewModel.Albums = ServiceHelper.ConvertToAlbumViewModels(Factory, albums, currencyCode, priceLevelId, userId);
            }

            return trackAlbumsListViewModel;
        }

        /// <summary>
        /// Return all tracks that the specified user have bought.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// All tracks that the specified user have bought.
        /// </returns>
        public async Task<ICollection<PurchasedTrackViewModel>> GetPurchasedTracksAsync(int userId)
        {
            ICollection<Track> tracks;
            using (var repository = Factory.GetPurchasedTrackRepository())
            {
                var purchasedTracks = await repository.GetAllAsync(
                                                          p => p.UserId == userId, p => p.Track, p => p.Track.Artist,
                                                          p => p.Track.Genre)
                                                      .ConfigureAwait(false);
                tracks = purchasedTracks.Select(p => p.Track).ToList();
            }

            // TODO: implement CreatePurchasedTracksListAsync method
            var trackViewModels = CreatePurchasedTracksList(tracks);
            return trackViewModels;
        }

        /// <summary>
        /// Return all tracks that the specified user have bought.
        /// </summary>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="pageSize">
        /// The number of the items on the page.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// All tracks that the specified user have bought.
        /// </returns>
        public PagedResult<PurchasedTrackViewModel> GetPurchasedTracks(int page, int pageSize, int userId)
        {
            PagedResult<PurchasedTrack> tracks;
            using (var repository = Factory.GetPurchasedTrackRepository())
            {
                tracks = repository.GetAll(page, pageSize, p => p.UserId == userId, p => p.Track, p => p.Track.Artist, p => p.Track.Genre);
            }

            var trackViewModels = CreatePurchasedTracksList(tracks.Items.Select(t => t.Track).ToList());
            return new PagedResult<PurchasedTrackViewModel>(trackViewModels, pageSize, page, tracks.TotalItemsCount);
        }

        private TrackAlbumsListViewModel CreateTrackAlbumsListViewModel(int trackId, int? currencyCode, int? priceLevel = null, int? userId = null)
        {
            return new TrackAlbumsListViewModel
            {
                TrackDetails = GetTrackDetails(trackId, currencyCode, priceLevel, userId)
            };
        }

        private ICollection<PurchasedTrackViewModel> CreatePurchasedTracksList(ICollection<Track> tracks)
        {
            var trackViewModels = new List<PurchasedTrackViewModel>();
            using (var repository = Factory.GetVoteRepository())
            {
                foreach (var track in tracks)
                {
                    var trackViewModel = ModelsMapper.GetPurchasedTrackViewModel(track);
                    if (trackViewModel != null)
                    {
                        trackViewModel.Rating = repository.GetAverageMark(track.Id);
                        trackViewModels.Add(trackViewModel);
                    }
                }
            }

            return trackViewModels;
        }

        private TrackDetailsViewModel CreateTrackDetailsViewModel(Track track, int? currencyCode = null, int? priceLevelId = null, int? userId = null)
        {
            var trackViewModel = ModelsMapper.GetTrackDetailsViewModel(track);

            if (currencyCode == null)
            {
                currencyCode = ServiceHelper.GetDefaultCurrency(Factory).Code;
            }

            if (priceLevelId == null)
            {
                priceLevelId = ServiceHelper.GetDefaultPriceLevel(Factory);
            }

            using (var repository = Factory.GetTrackPriceRepository())
            {
                using (var currencyRatesrepository = Factory.GetCurrencyRateRepository())
                {
                    trackViewModel.Price =
                            PriceHelper.GetTrackPrice(repository, currencyRatesrepository, track.Id, currencyCode.Value, priceLevelId.Value);
                }
            }

            trackViewModel.Rating = ServiceHelper.CalculateTrackRating(Factory, track.Id);

            using (var repository = Factory.GetAlbumTrackRelationRepository())
            {
                trackViewModel.AlbumsCount = repository.Count(r => r.TrackId == track.Id);
            }

            if (userId != null)
            {
                using (var repository = Factory.GetOrderTrackRepository())
                {
                    trackViewModel.IsOrdered =
                            repository.Exist(o => o.Cart.UserId == userId && o.TrackId == trackViewModel.Id);
                }

                using (var repository = Factory.GetPurchasedTrackRepository())
                {
                    trackViewModel.IsPurchased =
                            repository.Exist(p => p.UserId == userId && p.TrackId == trackViewModel.Id);
                }
            }

            return trackViewModel;
        }
    }
}
