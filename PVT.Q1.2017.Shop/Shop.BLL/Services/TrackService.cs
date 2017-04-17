namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using Common.ViewModels;
    using DAL.Infrastruture;
    using Exceptions;
    using Helpers;
    using Infrastructure;

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
            using (var repository = this.Factory.GetTrackRepository())
            {
                track = repository.GetById(id, t => t.Artist, t => t.Genre);
            }

            var trackViewModel = ModelsMapper.GetTrackDetailsViewModel(track);

            if (currencyCode == null)
            {
                currencyCode = ServiceHelper.GetDefaultCurrency(this.Factory).Code;
            }

            if (priceLevelId == null)
            {
                priceLevelId = ServiceHelper.GetDefaultPriceLevel(this.Factory);
            }

            using (var repository = this.Factory.GetTrackPriceRepository())
            {
                using (var currencyRatesrepository = Factory.GetCurrencyRateRepository())
                {
                    trackViewModel.Price =
                        PriceHelper.GetTrackPrice(repository, currencyRatesrepository, id, currencyCode.Value, priceLevelId.Value);
                }
            }

            trackViewModel.Rating = ServiceHelper.CalculateTrackRating(this.Factory, id);

            using (var repository = this.Factory.GetAlbumTrackRelationRepository())
            {
                trackViewModel.AlbumsCount = repository.Count(r => r.TrackId == id);
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

        /// <summary>
        /// Returns all registered tracks using the specified currency and price level for track price.
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
        /// All registered tracks.
        /// </returns>
        public ICollection<TrackViewModel> GetTracksList(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.Artist, t => t.Genre);
            }

            return ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel, userId);
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
        public ICollection<TrackDetailsViewModel> GetDetailedTracksList(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.Artist, t => t.Genre);
            }

            List<TrackDetailsViewModel> detailedList = new List<TrackDetailsViewModel>();
            foreach (var track in tracks)
            {
                detailedList.Add(GetTrackDetails(track.Id, currencyCode, priceLevel, userId));
            }

            return detailedList;
        }

        /// <summary>
        /// Returns all tracks which don't have price.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        public ICollection<TrackViewModel> GetTracksWithoutPrice()
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => !t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }

            return tracks.Select(ModelsMapper.GetTrackViewModel).ToList();
        }

        /// <summary>
        /// Returns all tracks with price specified using the specified currency and price level for track price.
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
        /// All tracks with price specified.
        /// </returns>
        public ICollection<TrackViewModel> GetTracksWithPrice(int? currencyCode = null, int? priceLevel = null, int? userId = null)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }

            return ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel, userId);
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
        public TrackAlbumsListViewModel GetAlbumsList(int trackId, int? currencyCode = null, int? priceLevelId = null, int? userId = null)
        {
            TrackAlbumsListViewModel trackAlbumsListViewModel = this.CreateTrackAlbumsListViewModel(trackId, currencyCode, priceLevelId, userId);

            if (trackAlbumsListViewModel.TrackDetails.AlbumsCount > 0)
            {
                ICollection<Album> albums;
                using (var repository = this.Factory.GetAlbumRepository())
                {
                    albums = repository.GetAll(a => a.Tracks.Any(t => t.TrackId == trackId), a => a.Artist);
                }

                trackAlbumsListViewModel.Albums = ServiceHelper.ConvertToAlbumViewModels(this.Factory, albums, currencyCode, priceLevelId, userId);
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
        public ICollection<PurchasedTrackViewModel> GetPurchasedTracks(int userId)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetPurchasedTrackRepository())
            {
                tracks = repository.GetAll(p => p.UserId == userId, p => p.Track, p => p.Track.Artist).Select(p => p.Track).ToList();
            }

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

        /// <summary>
        /// Creates a new instance of the <see cref="TrackAlbumsListViewModel"/> type.
        /// </summary>
        /// <param name="trackId">
        /// The track id.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="TrackAlbumsListViewModel"/> type.
        /// </returns>
        /// <exception cref="EntityNotFoundException{T}">
        /// When a track with the specified id doesn't exist.
        /// </exception>
        private TrackAlbumsListViewModel CreateTrackAlbumsListViewModel(int trackId, int? currencyCode, int? priceLevel = null, int? userId = null)
        {
            return new TrackAlbumsListViewModel
            {
                TrackDetails = GetTrackDetails(trackId, currencyCode, priceLevel, userId)
            };
        }
    }
}
