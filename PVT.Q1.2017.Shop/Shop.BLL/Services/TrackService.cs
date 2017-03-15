namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Common.Models;
    using Common.Models.ViewModels;
    using DAL.Infrastruture;
    using Exceptions;
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
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public ICollection<Track> GetTracksList()
        {
            using (var repository = Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all tracks without price configured.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        public ICollection<Track> GetTracksWithoutPriceConfigured()
        {
            using (var repository = Factory.GetTrackRepository())
            {
                return repository.GetAll(t => !t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks without price specified.
        /// </returns>
        public ICollection<Track> GetTracksWithPriceConfigured()
        {
            using (var repository = Factory.GetTrackRepository())
            {
                return repository.GetAll(t => t.TrackPrices.Any(), t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns the track with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// The track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        public Track GetTrack(int id)
        {
            using (var repository = Factory.GetTrackRepository())
            {
                return repository.GetById(id, t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns all track prices for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">The track</param>
        /// <param name="priceLevel">The price level.</param>
        /// <returns>All track prices for the specified  <paramref name="priceLevel"/>.</returns>
        public ICollection<TrackPrice> GetTrackPrices(Track track, PriceLevel priceLevel)
        {
            using (var repository = Factory.GetTrackPriceRepository())
            {
                return repository.GetAll(
                                         p => p.TrackId == track.Id &&
                                              p.PriceLevelId == priceLevel.Id,
                                         p => p.Currency);
            }
        }

        /// <summary>
        /// Returns all <paramref name="track"/> prices.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns>All <paramref name="track"/> prices>.</returns>
        public ICollection<TrackPrice> GetTrackPrices(Track track)
        {
            using (var repository = Factory.GetTrackPriceRepository())
            {
                return repository.GetAll(
                                         p => p.TrackId == track.Id,
                                         p => p.Currency,
                                         p => p.PriceLevel);
            }
        }

        /// <summary>
        /// Returns all track votes.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// All track votes.
        /// </returns>
        public ICollection<Vote> GetTrackVotes(Track track)
        {
            using (var repository = Factory.GetVoteRepository())
            {
                return repository.GetAll(v => v.TrackId == track.Id, v => v.Track, v => v.User);
            }
        }

        /// <summary>
        /// Returns all track feedbacks.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// All track feedbacks.
        /// </returns>
        public ICollection<Feedback> GetTrackFeedbacks(Track track)
        {
            using (var repository = Factory.GetFeedbackRepository())
            {
                return repository.GetAll(f => f.TrackId == track.Id, f => f.Track, f => f.User);
            }
        }

        /// <summary>
        /// Returns all albums whitch contain the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <param name="currency">The currency whitch is used to load album price.</param>
        /// <param name="priceLevel">The price level whitch is used to load album price.</param>
        /// <returns>
        /// All albums whitch contain the specified <paramref name="track"/>.
        /// </returns>
        public TrackAlbumListViewModel GetAlbumsList(Track track, Currency currency, PriceLevel priceLevel)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            if (priceLevel == null)
            {
                throw new ArgumentNullException(nameof(priceLevel));
            }

            Track existentTrack;
            using (var repository = Factory.GetTrackRepository())
            {
                existentTrack = repository.GetById(track.Id, t => t.Artist);
            }

            if (existentTrack == null)
            {
                throw new EntityNotFoundException<Track>(track, $"The specified track with id='{ track.Id }' is not found");
            }

            TrackAlbumListViewModel trackAlbumListViewModel = this.CreateTrackAlbumListViewModel(existentTrack);
            this.LoadTrackAlbums(trackAlbumListViewModel);
            this.LoadTrackAlbumsPrice(trackAlbumListViewModel, currency, priceLevel);

            return trackAlbumListViewModel;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TrackAlbumListViewModel"/> using the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="TrackAlbumListViewModel"/>.
        /// </returns>
        private TrackAlbumListViewModel CreateTrackAlbumListViewModel(Track track)
        {
            TrackAlbumListViewModel trackAlbumListViewModel = new TrackAlbumListViewModel
            {
                TrackId = track.Id,
                TrackName = track.Name
            };

            if (track.Artist != null)
            {
                trackAlbumListViewModel.ArtistId = track.Artist.Id;
                trackAlbumListViewModel.ArtistName = track.Artist.Name;
            }

            return trackAlbumListViewModel;
        }

        /// <summary>
        /// Loads from Db all albums which contain the track.
        /// </summary>
        /// <param name="trackAlbumListViewModel">
        /// The track album list view model.
        /// </param>
        private void LoadTrackAlbums(TrackAlbumListViewModel trackAlbumListViewModel)
        {
            using (var repository = Factory.GetAlbumRepository())
            {
                var albums = repository.GetAll(a => a.Tracks.Any(t => t.TrackId == trackAlbumListViewModel.TrackId));
                foreach (var album in albums)
                {
                    var albumViewModel = Mapper.Map<AlbumViewModel>(album);
                    albumViewModel.ArtistId = trackAlbumListViewModel.ArtistId;
                    albumViewModel.ArtistName = trackAlbumListViewModel.ArtistName;

                    trackAlbumListViewModel.Albums.Add(albumViewModel);
                }
            }
        }

        /// <summary>
        /// Loads price in the specified <paramref name="currency"/> and for the specified <paramref name="priceLevel"/> for each album.
        /// </summary>
        /// <param name="trackAlbumListViewModel">
        /// The track album list view model.
        /// </param>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        private void LoadTrackAlbumsPrice(TrackAlbumListViewModel trackAlbumListViewModel, Currency currency, PriceLevel priceLevel)
        {
            using (var repository = Factory.GetAlbumPriceRepository())
            {
                foreach (var album in trackAlbumListViewModel.Albums)
                {
                    album.Price = repository.GetAll(p => p.CurrencyId == currency.Id &&
                                                         p.PriceLevelId == priceLevel.Id)
                                            .FirstOrDefault();
                }
            }
        }
    }
}