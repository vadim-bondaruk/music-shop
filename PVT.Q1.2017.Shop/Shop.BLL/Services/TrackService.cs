namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Helpers;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Models;
    using Shop.Infrastructure.Validators;

    /// <summary>
    /// The track service
    /// </summary>
    public class TrackService : Service<ITrackRepository, Track>, ITrackService
    {
        #region Fields

        /// <summary>
        /// Default includes
        /// </summary>
        private readonly Expression<Func<Track, BaseEntity>>[] _defaultIncludes;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">The track validator.</param>
        public TrackService(IFactory factory, IValidator<Track> validator) : base(factory, validator)
        {
            this._defaultIncludes = new Expression<Func<Track, BaseEntity>>[]
            {
                t => t.Album, t => t.Artist, t => t.Genre
            };
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
            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(this._defaultIncludes);
            }
        }

        /// <summary>
        /// Returns all tracks without price configured.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        public ICollection<Track> GetTracksWithoutPrice()
        {
            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => !t.TrackPrices.Any(), this._defaultIncludes);
            }
        }

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks without price specified.
        /// </returns>
        public ICollection<Track> GetTracksWithPrice()
        {
            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetAll(t => t.TrackPrices.Any(), this._defaultIncludes);
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
            using (var repository = this.Factory.Create<ITrackRepository>())
            {
                return repository.GetById(id, this._defaultIncludes);
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
            ValidatorHelper.CheckTrackForNull(track);
            ValidatorHelper.CheckPriceLevelForNull(priceLevel);

            using (var repository = this.Factory.Create<ITrackPriceRepository>())
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
            ValidatorHelper.CheckTrackForNull(track);

            using (var repository = this.Factory.Create<ITrackPriceRepository>())
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
            ValidatorHelper.CheckTrackForNull(track);

            using (var repository = this.Factory.Create<IVoteRepository>())
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
            ValidatorHelper.CheckTrackForNull(track);

            using (var repository = this.Factory.Create<IFeedbackRepository>())
            {
                return repository.GetAll(f => f.TrackId == track.Id, f => f.Track, f => f.User);
            }
        }

        #endregion //ITrackService Members
    }
}