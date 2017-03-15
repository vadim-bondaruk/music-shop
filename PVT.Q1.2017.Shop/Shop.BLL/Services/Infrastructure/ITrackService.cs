namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.Models;
    using Common.Models.ViewModels;

    /// <summary>
    /// The track service
    /// </summary>
    public interface ITrackService
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
        ICollection<Track> GetTracksWithoutPriceConfigured();

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks with price specified.
        /// </returns>
        ICollection<Track> GetTracksWithPriceConfigured();

        /// <summary>
        /// Returns the track with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// The track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        Track GetTrack(int id);

        /// <summary>
        /// Returns all track prices for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="track">The track</param>
        /// <param name="priceLevel">The price level.</param>
        /// <returns>All track prices for the specified  <paramref name="priceLevel"/>.</returns>
        ICollection<TrackPrice> GetTrackPrices(Track track, PriceLevel priceLevel);

        /// <summary>
        /// Returns all <paramref name="track"/> prices.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns>All <paramref name="track"/> prices>.</returns>
        ICollection<TrackPrice> GetTrackPrices(Track track);

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

        /// <summary>
        /// Returns all albums whitch contain the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <param name="currency">The currency whitch is used to load album price.</param>
        /// <param name="priceLevel">The price level whitch is used to load album price.</param>
        /// <returns>
        /// All albums whitch contain the specified <paramref name="track"/>.
        /// </returns>
        TrackAlbumListViewModel GetAlbumsList(Track track, Currency currency, PriceLevel priceLevel);
    }
}