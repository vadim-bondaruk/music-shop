namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The track service
    /// </summary>
    public class TrackService : BaseService, ITrackService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The  repositories factory.
        /// </param>
        public TrackService(IRepositoryFactory factory) : base(factory)
        {
        }

        #endregion //Constructors

        #region ITrackService Members

        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public ICollection<Track> GetTracksList()
        {
            using (var repository = this.Factory.GetTrackRepository())
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
            using (var repository = this.Factory.GetTrackRepository())
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
            using (var repository = this.Factory.GetTrackRepository())
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
        public Track GetTrackInfo(int id)
        {
            using (var repository = this.Factory.GetTrackRepository())
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
            using (var repository = this.Factory.GetTrackPriceRepository())
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
            using (var repository = this.Factory.GetTrackPriceRepository())
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
            using (var repository = this.Factory.GetVoteRepository())
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
            using (var repository = this.Factory.GetFeedbackRepository())
            {
                return repository.GetAll(f => f.TrackId == track.Id, f => f.Track, f => f.User);
            }
        }

        /// <summary>
        /// Returns all albums whitch contain the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// All albums whitch contain the specified <paramref name="track"/>.
        /// </returns>
        public ICollection<Album> GetAllAlbumsWithTrack(Track track)
        {
            using (var repository = this.Factory.GetAlbumRepository())
            {
                return repository.GetAll(a => a.Tracks.Any(t => t.Id == track.Id));
            }
        }

        #endregion //ITrackService Members
    }
}