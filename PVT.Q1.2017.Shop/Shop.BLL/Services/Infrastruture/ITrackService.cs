namespace Shop.BLL.Services.Infrastruture
{
    using System.Collections.Generic;
    using Common.Models;
    using Infrastructure.Services;

    /// <summary>
    /// The track service
    /// </summary>
    public interface ITrackService : IService<Track>
    {
        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        ICollection<Track> GetTracksList();

        /// <summary>
        /// Returns all tracks without price configured.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        ICollection<Track> GetTracksWithoutPrice();

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks without price specified.
        /// </returns>
        ICollection<Track> GetTracksWithPrice();

        /// <summary>
        /// Returns the track with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The track id.
        /// </param>
        /// <returns>
        /// The track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        Track GetTrackInfo(int id);

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
        TrackPrice GeTrackPrice(Track track, PriceLevel priceLevel, Currency currency);

        /// <summary>
        /// Returns all available track price for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <returns>
        /// The track prices for the specified  <paramref name="priceLevel"/>.
        /// </returns>
        ICollection<TrackPrice> GetTrackPrices(Track track, PriceLevel priceLevel);

        /// <summary>
        /// Returns all track votes.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// All track votes.
        /// </returns>
        ICollection<Vote> GetTrackVotes(Track track);

        /// <summary>
        /// Returns all track feedbacks.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// All track feedbacks.
        /// </returns>
        ICollection<Feedback> GetTrackFeedbacks(Track track);
    }
}