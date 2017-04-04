﻿namespace Shop.BLL.Helpers
{
    using System.Collections.Generic;
    using Common.Models;
    using Common.ViewModels;
    using DAL.Infrastruture;

    /// <summary>
    /// The service helper
    /// </summary>
    internal static class ServiceHelper
    {
        /// <summary>
        /// Returns the default currency.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <returns>
        /// The default currency.
        /// </returns>
        internal static CurrencyViewModel GetDefaultCurrency(IRepositoryFactory repositoryFactory)
        {
            Currency currencyDto;
            using (var repository = repositoryFactory.GetCurrencyRepository())
            {
                currencyDto = repository.GetDefaultCurrency();
            }

            return ModelsMapper.GetCurrencyViewModel(currencyDto);
        }

        /// <summary>
        /// Returns the default price level.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <returns>
        /// The default price level.
        /// </returns>
        internal static int GetDefaultPriceLevel(IRepositoryFactory repositoryFactory)
        {
            PriceLevel priceLevelDto;
            using (var repository = repositoryFactory.GetPriceLevelRepository())
            {
                priceLevelDto = repository.GetDefaultPriceLevel();
            }

            return priceLevelDto.Id;
        }

        /// <summary>
        /// Converts each album to <see cref="AlbumViewModel"/>.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="albums">
        /// The albums.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <returns>
        /// A new collection with <see cref="AlbumViewModel"/> items.
        /// </returns>
        internal static ICollection<AlbumViewModel> ConvertToAlbumViewModels(
            IRepositoryFactory factory,
            ICollection<Album> albums,
            int? currencyCode,
            int? priceLevel)
        {
            if (currencyCode == null)
            {
                currencyCode = GetDefaultCurrency(factory).Code;
            }

            if (priceLevel == null)
            {
                priceLevel = GetDefaultPriceLevel(factory);
            }

            List<AlbumViewModel> albumViewModels = new List<AlbumViewModel>();
            using (var repository = factory.GetAlbumPriceRepository())
            {
                using (var currencyRatesrepository = factory.GetCurrencyRateRepository())
                {
                    foreach (var album in albums)
                    {
                        var albumViewModel = ModelsMapper.GetAlbumViewModel(album);
                        if (albumViewModel != null)
                        {
                            albumViewModel.Price =
                                PriceHelper.GetAlbumPrice(
                                    repository, currencyRatesrepository, albumViewModel.Id, currencyCode.Value, priceLevel.Value);
                            albumViewModels.Add(albumViewModel);
                        }
                    }
                }
            }

            using (var repository = factory.GetAlbumTrackRelationRepository())
            {
                foreach (var albumViewModel in albumViewModels)
                {
                    albumViewModel.TracksCount = repository.Count(r => r.AlbumId == albumViewModel.Id);
                }
            }

            return albumViewModels;
        }

        /// <summary>
        /// Converts each track to <see cref="TrackViewModel"/>.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="tracks">
        /// The tracks.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <returns>
        /// A new collection with <see cref="TrackViewModel"/> items.
        /// </returns>
        internal static ICollection<TrackViewModel> ConvertToTrackViewModels(
            IRepositoryFactory factory,
            ICollection<Track> tracks,
            int? currencyCode,
            int? priceLevel)
        {
            if (currencyCode == null)
            {
                currencyCode = GetDefaultCurrency(factory).Code;
            }

            if (priceLevel == null)
            {
                priceLevel = GetDefaultPriceLevel(factory);
            }

            List<TrackViewModel> trackViewModels = new List<TrackViewModel>();

            using (var repository = factory.GetTrackPriceRepository())
            {
                using (var currencyRatesrepository = factory.GetCurrencyRateRepository())
                {
                    foreach (var track in tracks)
                    {
                        var trackViewModel = ModelsMapper.GetTrackViewModel(track);
                        if (trackViewModel != null)
                        {
                            trackViewModel.Price =
                                PriceHelper.GetTrackPrice(
                                    repository, currencyRatesrepository, trackViewModel.Id, currencyCode.Value, priceLevel.Value);
                            trackViewModels.Add(trackViewModel);
                        }
                    }
                }
            }

            using (var repository = factory.GetVoteRepository())
            {
                trackViewModels.ForEach(t => t.Rating = repository.GetAverageMark(t.Id));
            }

            return trackViewModels;
        }

        /// <summary>
        /// Calculates the rating for the specified track.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        /// <param name="trackId">
        /// The track id.
        /// </param>
        /// <returns>
        /// The rating for the specified track.
        /// </returns>
        internal static RatingViewModel CalculateTrackRating(IRepositoryFactory factory, int trackId)
        {
            int votesCount;
            int commentsCount;
            double rating;
            using (var repository = factory.GetVoteRepository())
            {
                votesCount = repository.GetVotesCount(trackId);
                rating = repository.GetAverageMark(trackId);
            }

            using (var repository = factory.GetFeedbackRepository())
            {
                commentsCount = repository.GetFeedbacksCount(trackId);
            }

            return new RatingViewModel
            {
                VotesCount = votesCount,
                CommentsCount = commentsCount,
                Rating = rating
            };
        }
    }
}