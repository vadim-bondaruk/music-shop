﻿namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using Common.Models.ViewModels;
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
        /// <returns>
        /// The information about track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        public TrackDetailsViewModel GetTrackDetails(int id, int? currencyCode = null, int? priceLevelId = null)
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
                trackViewModel.Price = ServiceHelper.GetTrackPrice(repository, id, currencyCode.Value, priceLevelId.Value);
            }

            trackViewModel.Rating = ServiceHelper.CalculateTrackRating(this.Factory, id);

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
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public ICollection<TrackViewModel> GetTracksList(int? currencyCode = null, int? priceLevel = null)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.Artist, t => t.Genre);
            }

            return ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel);
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
        /// <returns>
        /// All tracks with price specified.
        /// </returns>
        public ICollection<TrackViewModel> GetTracksWithPrice(int? currencyCode = null, int? priceLevel = null)
        {
            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }

            return ServiceHelper.ConvertToTrackViewModels(this.Factory, tracks, currencyCode, priceLevel);
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
        /// <returns>
        /// All albums which contain the specified track.
        /// </returns>
        public TrackAlbumsListViewModel GetAlbumsList(int trackId, int? currencyCode = null, int? priceLevelId = null)
        {
            TrackAlbumsListViewModel trackAlbumsListViewModel = this.CreateTrackAlbumsListViewModel(trackId);

            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.Tracks.Any(t => t.TrackId == trackAlbumsListViewModel.Id));
            }

            trackAlbumsListViewModel.Albums = ServiceHelper.ConvertToAlbumViewModels(this.Factory, albums, currencyCode, priceLevelId);
            return trackAlbumsListViewModel;
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
        private TrackAlbumsListViewModel CreateTrackAlbumsListViewModel(int trackId)
        {
            Track track;
            using (var repository = this.Factory.GetTrackRepository())
            {
                track = repository.GetById(trackId);
            }

            return ModelsMapper.GetTrackAlbumsListViewModel(track);
        }
    }
}
