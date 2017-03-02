namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure.Repositories;
    using Infrastructure.Validators;
    using Infrastruture;

    /// <summary>
    /// The track service
    /// </summary>
    public class TrackService : Service<ITrackRepository, Track>, ITrackService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="validator">The track validator.</param>
        public TrackService(IRepositoryFactory repositoryFactory, IValidator<Track> validator) : base(repositoryFactory, validator)
        {
        }

        #endregion //Constructors

        #region Public Methods

        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        public ICollection<Track> GetTracksList()
        {
            using (var repository = this.RepositoryFactory.CreateRepository<ITrackRepository>())
            {
                return repository.GetAll(t => t.Album, t => t.Artist, t => t.Genre);
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks without price specified.
        /// </returns>
        public ICollection<Track> GetTracksWithPrice()
        {
            throw new NotImplementedException();
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
            using (var repository = this.RepositoryFactory.CreateRepository<ITrackRepository>())
            {
                return repository.GetById(id, t => t.Album, t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// Returns the track price in the specified <paramref name="currency"/> for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <returns>
        /// The track price in the specified currency for the specified  <paramref name="priceLevel"/> or <b>null</b>.
        /// </returns>
        public TrackPrice GeTrackPrice(Track track, PriceLevel priceLevel, Currency currency)
        {
            var trackPrices = this.GetTrackPrices(track, priceLevel);
            return trackPrices.FirstOrDefault(p => p.CurrencyId == currency.Id);
        }

        /// <summary>
        /// Returns all track prices for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">The track</param>
        /// <param name="priceLevel">The price level.</param>
        /// <returns>All track prices for the specified  <paramref name="priceLevel"/>.</returns>
        public ICollection<TrackPrice> GetTrackPrices(Track track, PriceLevel priceLevel)
        {
            using (var repository = this.RepositoryFactory.CreateRepository<ITrackPriceRepository>())
            {
                return repository.GetAll(p => p.TrackId == track.Id && p.PriceLevelId == priceLevel.Id);
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
            using (var repository = this.RepositoryFactory.CreateRepository<IVoteRepository>())
            {
                return repository.GetAll(v => v.Track.Id == track.Id, v => v.Track, v => v.User);
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
            using (var repository = this.RepositoryFactory.CreateRepository<IFeedbackRepository>())
            {
                return repository.GetAll(f => f.Track.Id == track.Id, f => f.Track, f => f.User);
            }
        }

        #endregion //Public Methods
    }
}